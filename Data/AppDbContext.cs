using Microsoft.EntityFrameworkCore;
using OffersManagement.Models;

namespace OffersManagement.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<Offer> Offers { get; set; }
        public DbSet<Product> Products { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    => optionsBuilder.UseCosmos(
        "https://happy-hal.documents.azure.com:443/",
        "sg0OCFec4RPk1NlhNHmWoYk03JhEce07owwnN2eWT5YcpwaZvXzw0UOZ2Bg5G9ajjukTmVlnYZUHACDbiovuAw==",
        databaseName: "OffersManagementDB");
    }
}
