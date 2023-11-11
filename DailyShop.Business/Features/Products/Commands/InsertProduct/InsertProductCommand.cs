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
		public string BodyImagePath { get; set; }
		public ICollection<string> ProductImagesPath { get; set; }
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
				Product product = new() { CategoryId = request.InsertedProductDto.CategoryId, BodyImage = request.BodyImagePath, Name = request.InsertedProductDto.Name, Price = request.InsertedProductDto.Price, Description = request.InsertedProductDto.Description, Status = request.InsertedProductDto.Status, Stock = request.InsertedProductDto.Stock };
				foreach (var productImage in request.ProductImagesPath)
				{
					ProductImage image = new() { Name = productImage };
					product.ProductImages?.Add(image);
				}
				if (request.InsertedProductDto.Colors != null && request.InsertedProductDto.Colors.Any())
					foreach (var productColor in request.InsertedProductDto.Colors)
					{
						Color color = new() { Name = productColor };
						product.Colors?.Add(color);
					}
				if (request.InsertedProductDto.Sizes != null && request.InsertedProductDto.Sizes.Any())
				{
					foreach (var productSize in request.InsertedProductDto.Sizes)
					{
						Size size = new() { Name = productSize };
						product.Sizes?.Add(size);
					}
				}
				product.UserId = request.UserId;
				await _productRepository.AddAsync(product);
			}
		}
	}
}
