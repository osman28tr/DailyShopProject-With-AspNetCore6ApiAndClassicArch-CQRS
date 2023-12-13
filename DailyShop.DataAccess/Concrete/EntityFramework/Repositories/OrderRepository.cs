using Core.Persistence.Repositories;
using Core.Security.Entities;
using DailyShop.Business.Services.Repositories;
using DailyShop.DataAccess.Concrete.EntityFramework.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DailyShop.Entities.Concrete;

namespace DailyShop.DataAccess.Concrete.EntityFramework.Repositories
{
    public class OrderRepository: EfRepositoryBase<Order, DailyShopContext>, IOrderRepository
    {
        public OrderRepository(DailyShopContext context) : base(context)
        {
        }
    }
}
