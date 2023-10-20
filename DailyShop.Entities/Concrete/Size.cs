using Core.Persistence.Repositories;

namespace DailyShop.Entities.Concrete
{
    public class Size:Entity
    {
        public string Name { get; set; }
        public Size()
        {
            Products = new HashSet<Product>();
        }
        public Size(int id):base(id)
        {
            Id = id;
        }
        public ICollection<Product> Products { get; set; }
    }
}
