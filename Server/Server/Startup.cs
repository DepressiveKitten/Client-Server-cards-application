namespace CardsApiApp
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using AutoMapper;
    using Cards.Services;
    using Cards.Services.InMemory;
    using Cards.Services.JsonStorage;
    using Cards.Services.JsonStorage.Serialization;
    using CardsApiApp.Controllers;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            ILoggerFactory loggerFactory = LoggerFactory.Create(builder =>
            {
                builder
                .AddConsole()
                .SetMinimumLevel(LogLevel.Information);
            });

            services.AddSingleton<ILogger>(loggerFactory.CreateLogger<CardsController>());
            services.AddControllers();
            services.AddControllersWithViews();
            services.AddDbContext<CardsContext>(opt =>
                opt.UseInMemoryDatabase("Cards"),ServiceLifetime.Singleton);
            services.AddSingleton((asd) =>
                new MapperConfiguration(mc =>
                {
                    mc.AddProfile(new MappingProfile());
                }).CreateMapper());
            services.AddSingleton<IJsonCardSerializer, JsonCardSerializer>();
            services.AddSingleton<ICardFileStorage, CardsJsonStorage>();
            services.AddSingleton<ICardStorageService, CardStorageService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IHostApplicationLifetime applicationLifetime, CardsContext context, ICardFileStorage fileStorage)
        {
            applicationLifetime.ApplicationStopping.Register(() => this.OnShutdown(app.ApplicationServices));

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            ContextSeeder.FillCards(context, fileStorage);
        }

        private void OnShutdown(IServiceProvider serviceProvider)
        {
            ICardFileStorage fileStorage = serviceProvider.GetService<ICardFileStorage>();
            ICardStorageService storageService = serviceProvider.GetService<ICardStorageService>();
            fileStorage.WriteAll(GetCards().Result);

            async Task<IEnumerable<Card>> GetCards()
            {
                List<Card> cards = new List<Card>();
                await foreach (var card in storageService.GetCardsAsync())
                {
                    cards.Add(card);
                }

                return cards;
            }
        }
    }
}
