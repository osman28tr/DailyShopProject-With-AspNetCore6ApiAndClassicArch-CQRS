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
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<AppUser>(a =>
			{
				a.ToTable("AppUsers");
				a.Property(p => p.Id).HasColumnName("Id");
			});
		}
	}
}
