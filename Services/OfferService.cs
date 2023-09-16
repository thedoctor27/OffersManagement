using Microsoft.Azure.Cosmos;
using Microsoft.EntityFrameworkCore;
using OffersManagement.Data;
using OffersManagement.Models;

namespace OffersManagement.Services
{
    public class OfferService : IOfferService
    {
        private readonly IDbContextFactory<AppDbContext> _dbFactory;
        private readonly IAppUserService _userService;
        private readonly IProductService _productService;

        public OfferService(IDbContextFactory<AppDbContext> dbFactory, IAppUserService userService,IProductService productService)
        {
            _dbFactory = dbFactory;
            _userService = userService;
            _productService = productService;
            using (var _context = dbFactory.CreateDbContext())
            {
                _context.Database.EnsureCreated();
            }
        }
        public async Task<bool> UserHasOfferToday(string userId,DateTime date)
        {
            using (var _context = _dbFactory.CreateDbContext())
            {
                string datestr = date.ToString("yyyyMMdd");
                return (await _context.Offers.Where(x => x.UserId == userId && x.DateStr == datestr && !x.IsArchived).CountAsync())>0;
            }
                
        }
        public async Task<List<Offer>> GetByUserId(string userId)
        {
            using (var _context = _dbFactory.CreateDbContext())
            {
                var offers = await _context.Offers.Where(x => x.UserId == userId).OrderByDescending(d => d.Date).ToListAsync();
                foreach(var offer in offers)
                {
                    //offer.User = await _userService.Get(offer.UserId); we don't need it this scope
                    foreach(var detail in offer.offerDetails)
                    {
                        detail.Product = await _productService.Get(detail.ProductId);
                    }
                }
                return offers;
            }
        }
        public async Task<string> Add(Offer Offer)
        {
            using (var _context = _dbFactory.CreateDbContext())
            {
                Offer.Number = (await _context.Offers.CountAsync())+1;
                _context.Entry(Offer.User).State = EntityState.Unchanged;
                foreach(var dt in Offer.offerDetails)
                {
                    _context.Entry(dt.Product).State = EntityState.Unchanged;
                }
                _context.Database.EnsureCreated();
                _context.Offers.Add(Offer);
                await _context.SaveChangesAsync();
            }
            return Offer.Id;
        }

        public async Task<string> Edit(string id, Offer UpdatedOffer)
        {
            using (var _context = _dbFactory.CreateDbContext())
            {
                var Offer = await _context.Offers.Where(x => x.Id == id).FirstOrDefaultAsync();
                if (Offer != null)
                {
                    Offer.Date = UpdatedOffer.Date;
                    Offer.DateStr = UpdatedOffer.DateStr;
                    Offer.IsArchived =  UpdatedOffer.IsArchived;
                    Offer.DateArchive = UpdatedOffer.DateArchive;
                    await _context.SaveChangesAsync();
                }
            }
            return id;
        }
        public async Task<string> Delete(string id)
        {
            using (var _context = _dbFactory.CreateDbContext())
            {
                var Offer = await _context.Offers.SingleOrDefaultAsync(x => x.Id == id);
                if (Offer != null)
                {
                    _context.Remove(Offer);
                    await _context.SaveChangesAsync();
                }
            }
            return id;
        }

        public async Task<Offer> Get(string id)
        {
            using (var _context = _dbFactory.CreateDbContext())
            {
                var offer = await _context.Offers.SingleOrDefaultAsync(x => x.Id == id);
                offer.User = await _userService.Get(offer.UserId);
                foreach (var detail in offer.offerDetails)
                {
                    detail.Product = await _productService.Get(detail.ProductId);
                }
                return offer;
            }
        }
        public async Task<List<Offer>> Get(DateTime date)
        {
            using (var _context = _dbFactory.CreateDbContext())
            {
                string datestr = date.ToString("yyyyMMdd");
                var offers = await _context.Offers.Where(d=>d.DateStr == datestr && !d.IsArchived).OrderByDescending(d => d.Date).ToListAsync();
                foreach (var offer in offers)
                {
                    offer.User = await _userService.Get(offer.UserId);
                    foreach (var detail in offer.offerDetails)
                    {
                        detail.Product = await _productService.Get(detail.ProductId);
                    }
                }
                return offers;
            }
        }
        public async Task<List<Offer>> GetArchived()
        {
            using (var _context = _dbFactory.CreateDbContext())
            {
                var offers = await _context.Offers.Where(d=>d.IsArchived).OrderByDescending(d => d.Date).ToListAsync();
                foreach (var offer in offers)
                {
                    offer.User = await _userService.Get(offer.UserId);
                    foreach (var detail in offer.offerDetails)
                    {
                        detail.Product = await _productService.Get(detail.ProductId);
                    }
                }
                return offers;
            }
        }
        public async Task StartArchiving()
        {
            using (var _context = _dbFactory.CreateDbContext())
            {
                var offers = await _context.Offers.Where(d=> !d.IsArchived).ToListAsync();
                foreach (var offer in offers)
                {
                    offer.IsArchived = true;
                    offer.DateArchive = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("Turkey Standard Time"));
                    await _context.SaveChangesAsync();
                }
            }
        }
    }
}
