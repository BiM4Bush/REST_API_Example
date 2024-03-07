using CsvHelper.Configuration.Attributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestApiExample.Models
{
    [Table("inventory")]
    public class Inventory
    {
        [Key]
        [Column("product_id")]
        [Name("product_id")]
        public string Product_Id { get; set; }

        [Column("sku")]
        [Name("sku")]
        public string SKU { get; set; }

        [Column("unit")]
        [Name("unit")]
        public string Unit { get; set; }

        [Column("qty")]
        [Name("qty")]
        public string Qty { get; set; }

        [Column("manufacturer")]
        [Name("manufacturer_name")]
        public string? Manufacturer { get; set; }

        [Column("shipping")]
        [Name("shipping")]
        public string? Shipping { get; set; }

        [Column("shipping_cost")]
        [Name("shipping_cost")]
        public string? Shipping_Cost { get; set; }

    }
}
