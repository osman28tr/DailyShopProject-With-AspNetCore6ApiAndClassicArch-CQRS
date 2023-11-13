using Core.Persistence.Repositories;

namespace DailyShop.Entities.Concrete
{
    public class Size:Entity
    {
        public string? Name { get; set; }
        public Size()
        {
            Products = new HashSet<ProductSize>();
        }
        public Size(int id):base(id)
        {
            Id = id;
        }
        public ICollection<ProductSize>? Products { get; set; }
    }
}
