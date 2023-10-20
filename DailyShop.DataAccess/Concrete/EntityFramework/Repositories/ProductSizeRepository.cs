using Core.Persistence.Repositories;
using DailyShop.Business.Services.Repositories;
using DailyShop.DataAccess.Concrete.EntityFramework.Contexts;
using DailyShop.Entities.Concrete;

namespace DailyShop.DataAccess.Concrete.EntityFramework.Repositories
{
    public class ProductSizeRepository : EfRepositoryBase<Size, DailyShopContext>, IProductSizeRepository
    {
        public ProductSizeRepository(DailyShopContext context) : base(context)
        {
        }
    }
}
