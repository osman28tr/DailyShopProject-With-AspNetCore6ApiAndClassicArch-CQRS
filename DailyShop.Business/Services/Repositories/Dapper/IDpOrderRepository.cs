using DailyShop.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyShop.Business.Services.Repositories.Dapper
{
    public interface IDpOrderRepository
    {
        public Task<List<Order>> GetList();
        public Task<List<OrderItem>> GetList(int orderId);
    }
}
