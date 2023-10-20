using Core.Persistence.Repositories;
using DailyShop.Entities.Concrete;

namespace DailyShop.Business.Services.Repositories
{
    public interface IProductImageRepository:IAsyncRepository<ProductImage>,IRepository<ProductImage>
    {
    }
}
