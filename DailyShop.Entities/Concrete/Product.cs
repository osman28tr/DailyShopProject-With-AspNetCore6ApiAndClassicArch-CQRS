using Core.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DailyShop.Entities.Concrete
{
    public class Product:Entity
    {
        public int? UserId { get; set; }
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
        public AppUser? User { get; set; }
        public ICollection<ProductImage>? ProductImages { get; set; }
        public ICollection<ProductColor>? Colors { get; set; }
        public ICollection<ProductSize>? Sizes { get; set; }
        public ICollection<Review>? Reviews { get; set; }
        public ICollection<CartItem>? CartItems { get; set; }
        public Product()
        {
            Colors = new HashSet<ProductColor>();
            Sizes = new HashSet<ProductSize>();
            ProductImages = new HashSet<ProductImage>();
        }
        public Product(int id):base(id)
        {
            Id = id;
        }
        public Product(int id, int categoryId,string name,decimal price,string bodyImage,string description,string status,int stock,byte rating,bool isDeleted,bool isApproved,int userId)
        {
            Id = id;
            CategoryId = categoryId;
            Name = name;
            Price = price;
            BodyImage = bodyImage;
            Description = description;
            Status = status;
            Stock = stock;
            Rating = rating;
            IsDeleted = isDeleted;
            IsApproved = isApproved;
            UserId = userId;
        }
    }
}
