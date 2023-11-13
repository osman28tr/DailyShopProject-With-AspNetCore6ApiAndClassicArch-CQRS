using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DailyShop.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DailyShop.DataAccess.Concrete.EntityFramework.EntityConfigurations
{
    public class ProductColorConfiguration:IEntityTypeConfiguration<ProductColor>
    {
        public void Configure(EntityTypeBuilder<ProductColor> builder)
        {
            builder.HasKey(pc => new { pc.ProductId, pc.ColorId });

            builder.HasOne(p => p.Product).WithMany(pc => pc.Colors).HasForeignKey(p => p.ProductId);

            builder.HasOne(c => c.Color).WithMany(pc => pc.Products).HasForeignKey(c => c.ColorId);
        }
    }
}
