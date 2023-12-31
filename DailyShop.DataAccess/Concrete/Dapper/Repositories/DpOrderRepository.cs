﻿using DailyShop.Business.Services.Repositories.Dapper;
using DailyShop.DataAccess.Concrete.Dapper.Contexts;
using DailyShop.Entities.Concrete;
using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyShop.DataAccess.Concrete.Dapper.Repositories
{
    public class DpOrderRepository : IDpOrderRepository
    {
        public SqlConnection connection = new SqlConnection(Context.Connection());
        public async Task<List<Order>> GetList()
        {
            return (await connection.QueryAsync<Order>(
                   "select * from orders"))
               .AsList();
        }

        public async Task<List<OrderItem>> GetList(int orderId)
        {
            return (await connection.QueryAsync<OrderItem>($"select * from OrderItems where OrderId = {orderId}"))
               .AsList();
        }
    }
}