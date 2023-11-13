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
    public class ProductSizeConfiguration: IEntityTypeConfiguration<ProductSize>
    {
        public void Configure(EntityTypeBuilder<ProductSize> builder)
        {
            builder.HasKey(ps => new { ps.ProductId, ps.SizeId });

            builder.HasOne(p => p.Product).WithMany(ps => ps.Sizes).HasForeignKey(p => p.ProductId);

            builder.HasOne(s => s.Size).WithMany(ps => ps.Products).HasForeignKey(s => s.SizeId);
        }
    }
}
