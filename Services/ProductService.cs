using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using OffersManagement.Data;
using OffersManagement.Models;

namespace OffersManagement.Services
{
    public class ProductService : IProductService
    {
        private readonly IDbContextFactory<AppDbContext> _dbFactory;

        public ProductService(IDbContextFactory<AppDbContext> dbFactory)
        {
            _dbFactory = dbFactory;

            using (var _context = dbFactory.CreateDbContext())
            {
                _context.Database.EnsureCreated();
            }
        }

        public async Task<string> Add(Product product)
        {
            using (var _context = _dbFactory.CreateDbContext())
            {
                product.Id = Guid.NewGuid().ToString();
                _context.Database.EnsureCreated();
                _context.Products.Add(product);
                await _context.SaveChangesAsync();
            }
            return product.Id;
        }

        public async Task<string> Edit(string id, Product UpdatedProduct)
        {
            using (var _context = _dbFactory.CreateDbContext())
            {
                var product = await _context.Products.Where(x => x.Id == id).FirstOrDefaultAsync();
                if (product != null)
                {
                    product.Name = UpdatedProduct.Name;
                    product.Type = UpdatedProduct.Type;
                    product.Subtype = UpdatedProduct.Subtype;
                    await _context.SaveChangesAsync();
                    return id;
                }
            }
            return "";
        }
        public async Task<string> Delete(string id)
        {
            using (var _context = _dbFactory.CreateDbContext())
            {
                var product = await _context.Products.SingleOrDefaultAsync(x => x.Id == id);
                if (product != null)
                {
                    _context.Remove(product);
                    await _context.SaveChangesAsync();
                    return id;
                }
            }
            return "";
        }

        public async Task<Product> Get(string id)
        {
            using (var _context = _dbFactory.CreateDbContext())
            {
                return await _context.Products.SingleOrDefaultAsync(x => x.Id == id);
            }
        }
        public async Task<List<Product>> Get()
        {
            using (var _context = _dbFactory.CreateDbContext())
            {
                return await _context.Products.ToListAsync();
            }
        }

    }
}
