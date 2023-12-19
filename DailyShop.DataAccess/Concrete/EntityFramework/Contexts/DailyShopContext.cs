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
        protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
	}
}
