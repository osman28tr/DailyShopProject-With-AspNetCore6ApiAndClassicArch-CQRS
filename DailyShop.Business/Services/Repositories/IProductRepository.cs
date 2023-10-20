using Core.Persistence.Repositories;
using DailyShop.Entities.Concrete;

namespace DailyShop.Business.Services.Repositories
{
    public interface IProductRepository:IAsyncRepository<Product>,IRepository<Product>
    {
    }
}
