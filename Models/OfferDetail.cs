using System.ComponentModel.DataAnnotations;

namespace OffersManagement.Models
{
    public class OfferDetail
    {
        [Key]
        public string Id { get; set; }
        public string OfferId { get; set; }
        [Required] 
        public string ProductId { get; set; }
        public Product Product { get; set; }
        [Required]
        public float Qte { get; set; }
        [Required]
        public float Price { get; set; }
    }
}
