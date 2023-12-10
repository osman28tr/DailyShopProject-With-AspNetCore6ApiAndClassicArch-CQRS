using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DailyShop.Business.Features.Products.Models;
using DailyShop.Entities.Concrete;

namespace DailyShop.Business.Services.Repositories.Dapper
{
    public interface IDpProductRepository
    {
        public Task<List<string>> GetProductDetailColorByIdAsync(int productId);
        public Task<List<string>> GetProductDetailSizeByIdAsync(int productId);
        public Task<List<string>> GetProductDetailImageByIdAsync(int productId);
        public Task<string> GetProductDetailUserByIdAsync(int productId);
    }
}
