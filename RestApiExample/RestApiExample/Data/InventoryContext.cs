using RestApiExample.Models;

namespace RestApiExample.Data
{
    public class InventoryContext : DbContext
    {
        public InventoryContext(DbContextOptions<InventoryContext> options) : base(options)
        {

        }
        public DbSet<Inventory> Inventories { get; set; }
    }
}
