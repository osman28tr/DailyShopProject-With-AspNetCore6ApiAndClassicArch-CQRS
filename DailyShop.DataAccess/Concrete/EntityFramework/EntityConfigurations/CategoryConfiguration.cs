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
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("Categories");
            builder.Property(p => p.Id).HasColumnName("Id");
            builder.HasMany(p => p.Products).WithOne(c => c.Category).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
