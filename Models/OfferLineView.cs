namespace OffersManagement.Models
{
    public class OfferLineView
    {
        public int LineId { get; set; }
        public int? OfferNumber { get; set; }
        public string OfferId { get; set; }
        public string CostumerId { get; set; }
        public string CostumerName { get; set; }
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public float  Price { get; set; }
        public float  Qte { get; set; }
        public DateTime OfferDate { get; set; }
    }
}
