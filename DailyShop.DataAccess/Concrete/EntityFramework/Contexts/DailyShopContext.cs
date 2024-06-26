﻿using Core.Security.Entities;
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
        public DbSet<ReportUser> ReportUsers { get; set; }
        public DbSet<ReportReview> ReportReviews { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            modelBuilder.Entity<CartItem>()
	            .Property(ci => ci.TotalPrice)
	            .HasColumnType("decimal(18, 2)");
            // Order
            modelBuilder.Entity<Order>()
	            .Property(o => o.TotalPrice)
	            .HasColumnType("decimal(18, 2)");
            // OrderItem
            modelBuilder.Entity<OrderItem>()
	            .Property(oi => oi.Price)
	            .HasColumnType("decimal(18, 2)");
            // product
            modelBuilder.Entity<Product>()
	            .Property(p => p.Price)
	            .HasColumnType("decimal(18, 2)");

		}
	}
}
