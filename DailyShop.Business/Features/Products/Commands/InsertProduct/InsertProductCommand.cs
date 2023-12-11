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
using Microsoft.EntityFrameworkCore;

namespace DailyShop.Business.Features.Products.Commands.InsertProduct
{
    public class InsertProductCommand : IRequest
    {
        public InsertedProductDto InsertedProductDto { get; set; }
        public string BodyImagePath { get; set; }
        public ICollection<string> ProductImagesPath { get; set; }
        public int? UserId { get; set; }
        public class InsertProductCommandHandler : IRequestHandler<InsertProductCommand>
        {
            private readonly IProductRepository _productRepository;
            private readonly IProductColorRepository _colorRepository;
            private readonly IProductSizeRepository _sizeRepository;
            private readonly IMapper _mapper;
            public InsertProductCommandHandler(IProductRepository productRepository, IMapper mapper, IProductColorRepository colorRepository, IProductSizeRepository sizeRepository)
            {
                _productRepository = productRepository;
                _mapper = mapper;
                _colorRepository = colorRepository;
                _sizeRepository = sizeRepository;
            }

            public async Task Handle(InsertProductCommand request, CancellationToken cancellationToken)
            {
                Product product = new() { CategoryId = request.InsertedProductDto.CategoryId, BodyImage = request.BodyImagePath, Name = request.InsertedProductDto.Name, Price = request.InsertedProductDto.Price, Description = request.InsertedProductDto.Description, Status = request.InsertedProductDto.Status, Stock = request.InsertedProductDto.Stock };

                List<Color> colors = await _colorRepository.Query().ToListAsync();
                List<Size> sizes = await _sizeRepository.Query().ToListAsync();

                if (request.ProductImagesPath != null && request.ProductImagesPath.Any())
                {
                    foreach (var productImage in request.ProductImagesPath)
                    {
                        ProductImage image = new() { Name = productImage };
                        product.ProductImages?.Add(image);
                    }
                }
                if (request.InsertedProductDto.Colors != null && request.InsertedProductDto.Colors.Any())
                    foreach (var productColor in request.InsertedProductDto.Colors)
                    {
                        if (colors.Find(x => x.Name.ToLower() == productColor.ToLower()) != null)
                        {
                            int colorId = colors.Find(x => x.Name.ToLower() == productColor.ToLower()).Id;
                            product.Colors?.Add(new ProductColor() { ColorId = colorId });
                        }
                        else
                        {
                            Color color = new() { Name = productColor };
                            product.Colors?.Add(new ProductColor() { Color = color });
                        }
                    }
                if (request.InsertedProductDto.Sizes != null && request.InsertedProductDto.Sizes.Any())
                {
                    foreach (var productSize in request.InsertedProductDto.Sizes)
                    {
                        if (sizes.Find(x => x.Name.ToLower() == productSize.ToLower()) != null)
                        {
                            int sizeId = sizes.Find(x => x.Name.ToLower() == productSize.ToLower()).Id;
                            product.Sizes?.Add(new ProductSize() { SizeId = sizeId });
                        }
                        else
                        {
                            Size size = new() { Name = productSize };
                            product.Sizes?.Add(new ProductSize() { Size = size });
                        }
                    }
                }
                product.UserId = request.UserId;
                product.IsDeleted = false;
                await _productRepository.AddAsync(product);
            }
        }
    }
}
