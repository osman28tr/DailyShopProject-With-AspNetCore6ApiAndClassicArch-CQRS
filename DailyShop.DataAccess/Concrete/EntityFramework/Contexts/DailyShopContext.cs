using Core.Security.Entities;
using DailyShop.DataAccess.Concrete.EntityFramework.EntityConfigurations;
using DailyShop.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DailyShop.DataAccess.Concrete.EntityFramework.Contexts
{
	public class DailyShopContext:DbContext
	{
        public DailyShopContext(DbContextOptions<DailyShopContext> options):base(options)
        { }
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<Size> Sizes { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<WebSiteSetting> WebSiteSettings { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<OrderAddress> OrderAddresses { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Favorite> Favorites { get; set; }
        public DbSet<Wallet> Wallets { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            Product[] productSeedData =
                { new(31,1, "deneme1", 140, "productimagess", "denemedes1", "yeni", 12, 6, false, true, 1),
                  new(32,1, "deneme2", 123, "productimagess2", "denemedes1", "yeni", 13, 2, false, true, 1),
                  new(33,1, "deneme3", 140, "productimagess3", "denemedes1", "yeni", 14, 3, false, true, 1),
                  new(34,1, "deneme4", 124, "productimagess4", "denemedes1", "yeni", 126, 4, false, true, 1),
                  new(35,1, "deneme5", 126, "productimagess5", "denemedes1", "yeni", 127, 5, false, true, 1),
                  new(36, 1, "deneme6", 167, "productimagess6", "denemedes1", "yeni", 129, 6, false, true, 1),
                  new(37,1, "deneme7", 156, "productimagess7", "denemedes1", "yeni", 12, 6, false, true, 2),
                  new(38, 1, "deneme8", 145, "productimagess8", "denemedes1", "yeni", 15, 6, false, true, 2),
                  new(39, 1, "deneme9", 167, "productimagess9", "denemedes1", "yeni", 12, 5, false, true, 2),
                  new(40,1, "deneme10", 140, "productimagess10", "denemedes1", "yeni", 21, 6, false, true, 2),
                  new(41, 1, "deneme11", 170, "productimagess11", "denemedes1", "yeni", 12, 7, false, true, 2),
                  new(42, 1, "deneme12", 190, "productimagess12", "denemedes1", "yeni", 24, 6, false, true, 2),
                  new(43,1, "deneme13", 167, "productimagess13", "denemedes1", "yeni", 126, 2, false, true, 2),
                  new(44, 1, "deneme14", 156, "productimagess14", "denemedes1", "yeni", 272, 1, false, true, 2),
                  new(45, 1, "deneme15", 145, "productimagess15", "denemedes1", "yeni", 30, 6, false, true, 2),
                  new(46, 1, "deneme16", 167, "productimagess16", "denemedes1", "yeni", 31, 3, false, true, 2),
                  new(47, 1, "deneme17", 178, "productimagess17", "denemedes1", "yeni", 36, 6, false, true, 2),
                  new(48, 1, "deneme18", 179, "productimagess18", "denemedes1", "yeni", 58, 6, false, true, 2),
                  new(49, 1, "deneme19", 145, "productimagess19", "denemedes1", "yeni", 78, 2, false, true, 2),
                  new(50, 1, "deneme20", 134, "productimagess20", "denemedes1", "yeni", 57, 6, false, true, 2),
                  new(51, 1, "deneme21", 145, "productimagess21", "denemedes1", "yeni", 46, 3, false, true, 2),
                  new(52, 1, "deneme22", 167, "productimagess22", "denemedes1", "yeni", 89, 6, false, true, 1),
                  new(53, 1, "deneme23", 189, "productimagess23", "denemedes1", "yeni", 57, 4, false, true, 1),
                  new(54, 1, "deneme24", 190, "productimagess24", "denemedes1", "yeni", 38, 6, false, true, 1),
                  new(55, 1, "deneme25", 145, "productimagess25", "denemedes1", "yeni", 77, 5, false, true, 3),
                  new(56, 1, "deneme26", 123, "productimagess26", "denemedes1", "yeni", 96, 6, false, true, 3),
                  new(57, 1, "deneme27", 112, "productimagess27", "denemedes1", "yeni", 65, 1, false, true, 3),
                  new(58, 1, "deneme28", 145, "productimagess28", "denemedes1", "yeni", 67, 3, false, true, 3),
                  new(59, 1, "deneme29", 167, "productimagess29", "denemedes1", "yeni", 47, 7, false, true, 3),
                  new(60, 1, "deneme30", 178, "productimagess30", "denemedes1", "yeni", 98, 6, false, true, 3),
                };
            modelBuilder.Entity<Product>().HasData(productSeedData);
        }
	}
}
