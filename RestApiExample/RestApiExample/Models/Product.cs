using System.ComponentModel.DataAnnotations.Schema;

namespace RestApiExample.Models
{
    [Table("products")]
    public class Product
    {
        [Column("id")]
        public int Id { get; set; }
        
        [Column("sku")]
        public string SKU { get; set; }
        
        [Column("name")]
        public string Name { get; set; }
        
        [Column("ean")]
        public string EAN { get; set; }

        [Column("producer_name")]
        public string Producer_Name { get; set;}

        [Column("category")]
        public string Category { get; set; }

        [Column("is_wire")]
        public bool Is_Wire { get; set; }

        [Column("shipping")]
        public DateTime Shipping { get; set; }

        [Column("available")]
        public bool Available { get; set; }

        [Column("is_vendor")]
        public bool Is_Vendor { get; set; }

        [Column("default_image")]
        public string Default_Image { get; set; }
    }
}
