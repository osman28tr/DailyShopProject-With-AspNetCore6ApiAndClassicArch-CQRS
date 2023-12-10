using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DailyShop.Business.Features.Products.Models;
using DailyShop.Business.Services.Repositories.Dapper;
using DailyShop.DataAccess.Concrete.Dapper.Contexts;
using DailyShop.Entities.Concrete;
using Dapper;
using Microsoft.Data.SqlClient;

namespace DailyShop.DataAccess.Concrete.Dapper.Repositories
{
    public class DpProductRepository:IDpProductRepository
    {
        private SqlConnection connection = new SqlConnection(Context.Connection());
        public async Task<List<string>> GetProductDetailColorByIdAsync(int productId)
        {
            return (await connection.QueryAsync<string>(
                    $"SELECT Colors.Name\r\nFROM dbo.Products LEFT JOIN dbo.ProductColor ON Products.Id = ProductColor.ProductId\r\nLEFT JOIN dbo.Colors ON ProductColor.ColorId = Colors.Id where Products.Id = {productId}"))
                .AsList();
        }

        public async Task<List<string>> GetProductDetailImageByIdAsync(int productId)
        {
            return (await connection.QueryAsync<string>(
                    $"select ProductImages.Name from Products inner join ProductImages on\r\nProducts.Id = ProductImages.ProductId where Products.Id = {productId}"))
                .AsList();
        }

        public async Task<List<string>> GetProductDetailSizeByIdAsync(int productId)
        {
            return (await connection.QueryAsync<string>(
                    $"SELECT Sizes.Name\r\nFROM Products LEFT JOIN ProductSize ON Products.Id = ProductSize.ProductId\r\nLEFT JOIN Sizes ON ProductSize.SizeId = Sizes.Id where Products.Id = {productId}"))
                .AsList();
        }

        public async Task<string> GetProductDetailUserByIdAsync(int productId)
        {
            return await connection.QueryFirstAsync<string>(
                $"select Users.FirstName + ' '  + Users.LastName as 'UserName' from Users inner join Products on Users.Id = Products.UserId\r\nwhere Products.Id = {productId}");
        }
    }
}
