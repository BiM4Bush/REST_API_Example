using System.ComponentModel.DataAnnotations.Schema;

namespace RestApiExample.Models
{
    [Table("inventory")]
    public class Inventory
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("sku")]
        public string SKU { get; set; }

        [Column("unit")]
        public string Unit { get; set; }

        [Column("qty")]
        public int Qty { get; set; }

        [Column("manufacturer")]
        public string Manufacturer { get; set; }

        [Column("shipping")]
        public DateTime Shipping { get; set; }

        [Column("shipping_cost")]
        public double Shipping_Cost { get; set; }

    }
}
