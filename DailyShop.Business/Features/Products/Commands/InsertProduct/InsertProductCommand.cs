using AutoMapper;
using DailyShop.Business.Features.Products.Dtos;
using DailyShop.Business.Features.Products.Models;
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
    public class InsertProductCommand : IRequest
    {
        public InsertedProductDto InsertedProductDto { get; set; }
        public int? UserId { get; set; }
        public class InsertProductCommandHandler : IRequestHandler<InsertProductCommand>
        {
            private readonly IProductRepository _productRepository;
            private readonly IMapper _mapper;
            public InsertProductCommandHandler(IProductRepository productRepository, IMapper mapper)
            {
                _productRepository = productRepository;
                _mapper = mapper;
            }

            public async Task Handle(InsertProductCommand request, CancellationToken cancellationToken)
            {
                Product product = _mapper.Map<Product>(request.InsertedProductDto);

                //product.ProductImages = new List<ProductImage>();
                //product.Colors = new List<Color>();
                //product.Sizes = new List<Size>();

                //ICollection<Color> productColors = _mapper.Map<ICollection<Color>>(request.InsertedProductDto.Colors);

                //ICollection<Size> productSizes = _mapper.Map<ICollection<Size>>(request.InsertedProductDto.Sizes);

                //ICollection<ProductImage> productImages = _mapper.Map<ICollection<ProductImage>>(request.InsertedProductDto.ProductImages);

                //foreach (var productColor in productColors)
                //{
                //    product.Colors.Add(productColor);
                //}
                //foreach (var productSize in productSizes)
                //{
                //    product.Sizes.Add(productSize);
                //}
                //foreach (var productImage in productImages)
                //{
                //    product.ProductImages.Add(productImage);
                //}
                //product.Sizes = productSizes;
                //product.ProductImages = productImages;
                product.UserId = request.UserId;
                await _productRepository.AddAsync(product);
                //var mappedProduct = _mapper.Map<InsertProductViewModel>(insertedProduct);
                //return mappedProduct;
            }
        }
    }
}
