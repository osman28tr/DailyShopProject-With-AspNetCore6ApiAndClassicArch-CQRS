using AutoMapper;
using DailyShop.Business.Features.ProductImages.Dtos;
using DailyShop.Business.Services.Repositories;
using DailyShop.Entities.Concrete;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyShop.Business.Features.ProductImages.Commands.InsertProductImage
{
    public class InsertProductImageCommand : IRequest
    {
        public ICollection<InsertedProductImageDto> InsertedProductImageDto { get; set; }
        public int ProductId { get; set; }
        public class ProductImageCommandHandler : IRequestHandler<InsertProductImageCommand>
        {
            private readonly IProductImageRepository _productImageRepository;
            private readonly IMapper _mapper;

            public ProductImageCommandHandler(IProductImageRepository productImageRepository, IMapper mapper)
            {
                _productImageRepository = productImageRepository;
                _mapper = mapper;
            }

            public async Task Handle(InsertProductImageCommand request, CancellationToken cancellationToken)
            {
                ICollection<ProductImage> productImages = _mapper.Map<ICollection<ProductImage>>(request.InsertedProductImageDto);
                foreach (var productImage in productImages)
                {
                    productImage.ProductId = request.ProductId;
                    await _productImageRepository.AddAsync(productImage);
                }              
            }
        }
    }
}
