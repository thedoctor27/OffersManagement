using Microsoft.EntityFrameworkCore;
using OffersManagement.Data;
using OffersManagement.Models;

namespace OffersManagement.Services
{
    public class AppUserService : IAppUserService
    {
        private readonly IDbContextFactory<AppDbContext> _dbFactory;

        public AppUserService(IDbContextFactory<AppDbContext> dbFactory)
        {
            _dbFactory = dbFactory;

            using (var _context = dbFactory.CreateDbContext())
            {
                _context.Database.EnsureCreated();
            }
        }

        public async Task<string> Add(AppUser AppUser)
        {
            using (var _context = _dbFactory.CreateDbContext())
            {
                _context.Database.EnsureCreated();
                _context.AppUsers.Add(AppUser);
                await _context.SaveChangesAsync();
            }
            return AppUser.Id;
        }

        public async Task<string> Edit(string id, AppUser UpdatedAppUser)
        {
            using (var _context = _dbFactory.CreateDbContext())
            {
                var AppUser = await _context.AppUsers.SingleOrDefaultAsync(x => x.Id == id);
                if (AppUser != null)
                {
                    AppUser.Name = UpdatedAppUser.Name;
                    AppUser.Email = UpdatedAppUser.Email;
                    await _context.SaveChangesAsync();
                }
            }
            return id;
        }
        public async Task<string> Delete(string id)
        {
            using (var _context = _dbFactory.CreateDbContext())
            {
                var AppUser = await _context.AppUsers.SingleOrDefaultAsync(x => x.Id == id);
                if (AppUser != null)
                {
                    _context.Remove(AppUser);
                    await _context.SaveChangesAsync();
                }
            }
            return id;
        }

        public async Task<AppUser> Get(string id)
        {
            using (var _context = _dbFactory.CreateDbContext())
            {
                return await _context.AppUsers.SingleOrDefaultAsync(x => x.Id == id);
            }
        }
        public async Task<List<AppUser>> Get()
        {
            using (var _context = _dbFactory.CreateDbContext())
            {
                return await _context.AppUsers.ToListAsync();
            }
        }
    }
}
