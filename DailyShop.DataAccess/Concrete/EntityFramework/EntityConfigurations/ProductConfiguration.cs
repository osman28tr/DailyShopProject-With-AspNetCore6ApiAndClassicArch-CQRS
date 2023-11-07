using DailyShop.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyShop.DataAccess.Concrete.EntityFramework.EntityConfigurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");
            builder.Property(p => p.Id).HasColumnName("Id");
            builder.HasOne(p => p.Category);
            builder.HasMany(p => p.ProductImages).WithOne(p => p.Product).OnDelete(DeleteBehavior.Cascade);
            //builder.HasMany(p=>p.Colors).WithMany(p=>p.Products).UsingEntity<Product>()
        }
    }
}
