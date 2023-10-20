using DailyShop.Business.Services.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyShop.Business.Features.Products.Queries.GetListProduct
{
    public class GetListProductQuery
    {
        private readonly IProductRepository _productRepository;

        public GetListProductQuery(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task Deneme()
        {
            
        }
    }
}
