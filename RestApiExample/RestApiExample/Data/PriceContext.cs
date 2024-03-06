using RestApiExample.Models;

namespace RestApiExample.Data
{
    public class PriceContext : DbContext
    {
        public PriceContext(DbContextOptions<PriceContext> options) : base(options)
        {
                
        }

        public DbSet<Price> Prices { get; set; }
    }
}
