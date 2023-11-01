using AutoMapper;
using DailyShop.Business.Features.Products.Dtos;
using DailyShop.Business.Services.Repositories;
using DailyShop.Entities.Concrete;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyShop.Business.Features.Products.Commands.InsertProduct
{
    public class InsertProductCommand:IRequest
    {
        public InsertedProductDto InsertedProductDto { get; set; }
        public class InsertProductCommandHandler : IRequestHandler<InsertProductCommand>
        {
            private readonly IProductRepository _productRepository;
            private readonly IMapper _mapper;
            public InsertProductCommandHandler(IProductRepository productRepository,IMapper mapper)
            {
                _productRepository = productRepository;
                _mapper = mapper;
            }

            public async Task Handle(InsertProductCommand request, CancellationToken cancellationToken)
            {
                Product product = _mapper.Map<Product>(request.InsertedProductDto);
                await _productRepository.AddAsync(product);
            }
        }
    }
}
