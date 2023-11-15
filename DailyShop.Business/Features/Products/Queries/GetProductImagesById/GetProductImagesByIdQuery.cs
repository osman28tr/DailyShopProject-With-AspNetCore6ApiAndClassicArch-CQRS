using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DailyShop.Business.Features.Products.Models;
using DailyShop.Business.Services.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DailyShop.Business.Features.Products.Queries.GetProductImagesById
{
    public class GetProductImagesByIdQuery:IRequest<GetProductImagesByIdViewModel>
    {
        public int ProductId { get; set; }
        public class GetProductImagesByIdQueryHandler:IRequestHandler<GetProductImagesByIdQuery,GetProductImagesByIdViewModel>
        {
            private readonly IProductRepository _productRepository;
            private readonly IMapper _mapper;

            public GetProductImagesByIdQueryHandler(IProductRepository productRepository, IMapper mapper)
            {
                _productRepository = productRepository;
                _mapper = mapper;
            }

            public async Task<GetProductImagesByIdViewModel> Handle(GetProductImagesByIdQuery request, CancellationToken cancellationToken)
            {
                var product = await _productRepository.Query().Where(p => p.Id == request.ProductId)
                    .Include(pi => pi.ProductImages).FirstOrDefaultAsync(cancellationToken: cancellationToken);
                var mappedProduct = _mapper.Map<GetProductImagesByIdViewModel>(product);
                return mappedProduct;
            }
        }
    }
}
