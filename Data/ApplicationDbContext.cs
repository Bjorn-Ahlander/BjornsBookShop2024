using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using BjornsBookShop2024.Models;

namespace BjornsBookShop2024.Data
{
    public class ApplicationDbContext : IdentityDbContext<BjornBookShopUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public ApplicationDbContext()
         {

         }
        public DbSet<Book> Books { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<BookOrder> BookOrders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

        //    // Lägg till eventuella ytterligare konfigurationer för Entity Framework här
        }
    }
}
