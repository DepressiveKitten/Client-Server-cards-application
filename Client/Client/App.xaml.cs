using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Windows;
using Client.Core;
using Client.ViewModel;
using Client.Model;
using Client.Stores;
using CardHttpClient;
using CardJsonDeserialization;
using System.IO;
using System.Net.Http;
using AutoMapper;

namespace Client
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private IServiceProvider Services { get; }

        public IConfiguration AppSettings { get; } = new ConfigurationBuilder()
                   .SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json")
                   .Build();

        public App()
        {
            Services = new ServiceCollection()
                .AddSingleton<ICardHttpClient, CardClient>()
                .AddSingleton<IJsonDeserializer, JsonCardSerializer>()
                .AddSingleton<HttpClient>()
                .AddSingleton((asd) =>
                new MapperConfiguration(mc =>
                {
                    mc.AddProfile(new MappingProfile());
                }).CreateMapper())

                .AddSingleton(this.AppSettings)

                .AddTransient(s => CreateCardCreationViewModel())
                .AddTransient(s => CreateCardListingViewModel())

                .AddSingleton<NavigationStore>()

                .BuildServiceProvider();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            Services.GetRequiredService<NavigationStore>().CurrentViewModel = Services.GetRequiredService<CardListingViewModel>();

            MainWindow = new MainWindow()
            {
                DataContext = new MainViewModel(Services.GetRequiredService<NavigationStore>())
            };

            MainWindow.Show();           

            base.OnStartup(e);
        }

        private CardCreationViewModel CreateCardCreationViewModel()
        {
            return new CardCreationViewModel(Services.GetRequiredService<NavigationStore>(),
                CreateCardListingViewModel,
                Services.GetRequiredService<ICardHttpClient>(),
                Services.GetRequiredService<IMapper>());
        }

        private CardListingViewModel CreateCardListingViewModel()
        {
            return new CardListingViewModel(Services.GetRequiredService<NavigationStore>(),
                CreateCardCreationViewModel,
                Services.GetRequiredService<ICardHttpClient>(),
                Services.GetRequiredService<IMapper>());
        }
    }
}
