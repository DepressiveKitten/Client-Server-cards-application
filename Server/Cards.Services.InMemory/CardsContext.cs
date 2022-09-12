using Microsoft.EntityFrameworkCore;

namespace Cards.Services.InMemory
{
    public class CardsContext : DbContext
    {
        public CardsContext(DbContextOptions<CardsContext> options)
            :base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Card> Cards { get; set; }
    }
}
