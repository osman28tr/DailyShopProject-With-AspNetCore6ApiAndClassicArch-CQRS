using Core.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyShop.Entities.Concrete
{
    public class Product:Entity
    {
        public int? CategoryId { get; set; }
        public string? Name { get; set; }
        public decimal? Price { get; set; }
        public string? BodyImage { get; set; }
        public string? Description { get; set; }
        public string? Status { get; set; }
        public int? Stock { get; set; }
        public byte? Rating { get; set; }
        public bool? IsDeleted { get; set; }
        public bool? IsApproved { get; set; }
        public Category? Category { get; set; }
        public ICollection<ProductImage>? ProductImages { get; set; }
        public ICollection<Color>? Colors { get; set; }
        public ICollection<Size>? Sizes { get; set; }
        public ICollection<Review>? Reviews { get; set; }
        public Product()
        {
            Colors = new HashSet<Color>();
            Sizes = new HashSet<Size>();
        }
        public Product(int id):base(id)
        {
            Id = id;
        }
    }
}
