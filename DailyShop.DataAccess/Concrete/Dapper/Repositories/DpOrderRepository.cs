using DailyShop.Business.Services.Repositories.Dapper;
using DailyShop.DataAccess.Concrete.Dapper.Contexts;
using DailyShop.Entities.Concrete;
using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;

namespace DailyShop.DataAccess.Concrete.Dapper.Repositories
{
    public class DpOrderRepository : IDpOrderRepository
    {
        public NpgsqlConnection connection = new(Context.Connection());
        public async Task<List<Order>> GetList()
        {
            return (await connection.QueryAsync<Order>(
                   """select * from "Orders" """)).Where(x => x != null)
                .ToList();
        }

        public async Task<List<OrderItem>> GetList(int orderId)
        {
            var orders = (await connection.QueryAsync<OrderItem>(""" select * from "OrderItems" where "OrderId" = @orderId""", new { orderId = orderId })).Where(x => x != null)
                .ToList();
            return orders;
        }
    }
}
