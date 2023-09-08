using System.ComponentModel.DataAnnotations;

namespace OffersManagement.Models
{
    public class AppUser
    {
        [Key]
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

    }
}
