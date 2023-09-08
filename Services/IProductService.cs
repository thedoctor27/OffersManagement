using OffersManagement.Models;

namespace OffersManagement.Services
{
    public interface IProductService
    {
        Task<string> Add(Product product);
        Task<string> Delete(string id);
        Task<string> Edit(string id, Product UpdatedProduct);
        Task<List<Product>> Get();
        Task<Product> Get(string id);
    }
}