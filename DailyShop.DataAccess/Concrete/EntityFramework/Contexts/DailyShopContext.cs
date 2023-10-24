using Core.Security.Entities;
using DailyShop.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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
        protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<AppUser>(a =>
			{
				a.ToTable("AppUsers");
				a.Property(p => p.Id).HasColumnName("Id");
				a.HasMany(p => p.Addresses);
			});
			modelBuilder.Entity<Address>(a =>
			{
				a.ToTable("Addresses");
				a.Property(p => p.Id).HasColumnName("Id");
				a.HasOne(p => p.AppUser);
			});
            modelBuilder.Entity<Category>(a =>
            {
                a.ToTable("Categories");
                a.Property(p => p.Id).HasColumnName("Id");
                a.HasMany(p => p.Products);
            });
            modelBuilder.Entity<Product>(a =>
            {
                a.ToTable("Products");
                a.Property(p => p.Id).HasColumnName("Id");
                a.HasOne(p => p.Category);
            });
            modelBuilder.Entity<Color>(a =>
            {
                a.ToTable("Colors");
                a.Property(p => p.Id).HasColumnName("Id");
            });
            modelBuilder.Entity<Size>(a =>
            {
                a.ToTable("Sizes");
                a.Property(p => p.Id).HasColumnName("Id");
            });
            modelBuilder.Entity<ProductImage>(a =>
            {
                a.ToTable("ProductImages");
                a.Property(p => p.Id).HasColumnName("Id");
                a.HasOne(p => p.Product);
            });
            modelBuilder.Entity<Review>(a =>
            {
                a.ToTable("Reviews");
                a.Property(p => p.Id).HasColumnName("Id");
            });
        }
	}
}
