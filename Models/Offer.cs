using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OffersManagement.Models
{
    public class Offer
    {
        [Key]
        public string Id { get; set; }
        public int Number { get; set; }
        public string UserId { get; set; }
        public AppUser User { get; set; }
        public string DateStr { get; set; }
        public DateTime Date { get; set; }

        public bool IsArchived { get; set; }
        public DateTime DateArchive { get; set; }

        public ICollection<OfferDetail> offerDetails { get; set; }
    }
}
