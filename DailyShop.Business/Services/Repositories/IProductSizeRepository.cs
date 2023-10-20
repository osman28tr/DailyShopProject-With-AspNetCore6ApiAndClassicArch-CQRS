using Core.Persistence.Repositories;
using DailyShop.Entities.Concrete;

namespace DailyShop.Business.Services.Repositories
{
    public interface IProductSizeRepository:IAsyncRepository<ProductSize>,IRepository<ProductSize>
    {
    }
}
