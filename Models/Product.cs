using System.ComponentModel.DataAnnotations;

namespace OffersManagement.Models
{
    public class Product
    {
        [Key]
        public string Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Type { get; set; }
        [Required]
        public string Subtype { get; set; }
    }
}
