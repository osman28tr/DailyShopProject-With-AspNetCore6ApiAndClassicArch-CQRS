using Core.Persistence.Repositories;

namespace DailyShop.Entities.Concrete
{
    public class ProductImage:Entity
    {
        public int? ProductId { get; set; }
        public string? Name { get; set; }
        public ProductImage()
        {
            
        }
        public ProductImage(int id):base(id)
        {
            Id = id;
        }
        public Product? Product { get; set; }
    }
}
