using OffersManagement.Models;

namespace OffersManagement.Services
{
    public interface IAppUserService
    {
        Task<string> Add(AppUser AppUser);
        Task<string> Delete(string id);
        Task<string> Edit(string id, AppUser UpdatedAppUser);
        Task<List<AppUser>> Get();
        Task<AppUser> Get(string id);
    }
}