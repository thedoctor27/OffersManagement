using OffersManagement.Models;

namespace OffersManagement.Services
{
    public interface IOfferService
    {
        Task<string> Add(Offer Offer);
        Task<string> Delete(string id);
        Task<string> Edit(string id, Offer UpdatedOffer);
        Task<List<Offer>> Get(DateTime date);
        Task<List<Offer>> GetByUserId(string userId);
        Task<Offer> Get(string id);
        Task<List<Offer>> GetArchived();
    }
}