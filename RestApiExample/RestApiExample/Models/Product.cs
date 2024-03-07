using CsvHelper.Configuration.Attributes;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestApiExample.Models
{
    [Table("products")]
    public class Product
    {
        [Column("ID")]
        [Name("ID")]
        public string ID { get; set; }
        
        [Column("sku")]
        [Name("SKU")]
        public string SKU { get; set; }
        
        [Column("name")]
        [Name("name")]
        public string Name { get; set; }
        
        [Column("ean")]
        [Name("EAN")]
        public string EAN { get; set; }

        [Column("producer_name")]
        [Name("producer_name")]
        public string Producer_Name { get; set;}

        [Column("category")]
        [Name("category")]
        public string Category { get; set; }

        [Column("is_wire")]
        [Name("is_wire")]
        public bool Is_Wire { get; set; } = false;

        [Column("shipping")]
        [Name("shipping")]
        public string Shipping { get; set; }

        [Column("available")]
        [Name("available")]
        public bool Available { get; set; } = false;

        [Column("is_vendor")]
        [Name("is_vendor")]
        public bool Is_Vendor { get; set; } = false;

        [Column("default_image")]
        [Name("default_image")]
        public string Default_Image { get; set; }
    }
}