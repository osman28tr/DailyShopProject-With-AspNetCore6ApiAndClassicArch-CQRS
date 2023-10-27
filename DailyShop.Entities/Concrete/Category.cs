using Core.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyShop.Entities.Concrete
{
    public class Category:Entity
    {
        public string? Name { get; set; }
        public int? ParentCategoryId { get; set; }
        public Category() { }
        public Category(int id):base(id)
        {
            Id = id;
        }
        public ICollection<Product>? Products { get; set; }
        [ForeignKey("ParentCategoryId")]
        public ICollection<Category>? SubCategories { get; set; }
    }
}
