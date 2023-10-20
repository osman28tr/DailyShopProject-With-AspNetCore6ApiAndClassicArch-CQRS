using Core.Persistence.Repositories;

namespace DailyShop.Entities.Concrete
{
    public class ProductSize:Entity
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public Product Product { get; set; }
        public ProductSize()
        {
            
        }
        public ProductSize(int id):base(id)
        {
            Id = id;
        }
    }
}
