using Microsoft.EntityFrameworkCore;
using RestApiExample.Models;

namespace RestApiExample.Data
{
    public class RestApiExDbContext : DbContext //name convention 
    {
        public RestApiExDbContext(DbContextOptions<RestApiExDbContext> options) : base(options)
        {
                
        }
        public DbSet<Inventory> Inventories { get; set; }
        public DbSet<Price> Prices { get; set; }
        public DbSet<Product> Products { get; set; }

    }
}
