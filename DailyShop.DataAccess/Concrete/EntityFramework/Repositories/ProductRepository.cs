using Core.Persistence.Repositories;
using DailyShop.Business.Services.Repositories;
using DailyShop.DataAccess.Concrete.EntityFramework.Contexts;
using DailyShop.Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DailyShop.DataAccess.Concrete.EntityFramework.Repositories
{
    public class ProductRepository : EfRepositoryBase<Product, DailyShopContext>, IProductRepository
    {
        public ProductRepository(DailyShopContext context) : base(context)
        {
            //context.Products.Include(p => p.Colors).ToList();
        }
    }
}
