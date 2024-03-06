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
        public int Nett_Price { get; set; }

        [Column("discount_nett_price")]
        public int Discount_Nett_Price { get; set; }

        [Column("vat_rate")]
        public int Vat_Rate { get; set;}

        [Column("logistic_unit_nett_price")]
        public int Logistic_Unit_Nett_Price { get; set; }

    }
}
