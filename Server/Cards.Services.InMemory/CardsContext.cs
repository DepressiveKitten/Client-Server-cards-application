namespace Cards.Services.InMemory
{
    using Microsoft.EntityFrameworkCore;

    public class CardsContext : DbContext
    {
        public CardsContext(DbContextOptions<CardsContext> options)
            : base(options)
        {
            this.Database.EnsureCreated();
        }

        public DbSet<Card> Cards { get; set; }
    }
}
