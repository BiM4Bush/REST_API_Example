using System.ComponentModel.DataAnnotations.Schema;

namespace RestApiExample.Models
{
    [Table("prices")]
    public class Price
    {
        [Column("id")]
        public string Id { get; set; }

        [Column("sku")]
        public string SKU { get; set; }

        [Column("nett_price")]
        public string Nett_Price { get; set; }

        [Column("discount_nett_price")]
        public string Discount_Nett_Price { get; set; }

        [Column("vat_rate")]
        public string Vat_Rate { get; set;}

        [Column("logistic_unit_nett_price")]
        public string Logistic_Unit_Nett_Price { get; set; }

    }
}
