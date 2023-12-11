using AutoMapper;
using DailyShop.Business.Features.Products.Models;
using DailyShop.Business.Services.Repositories;
using DailyShop.Entities.Concrete;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyShop.Business.Features.Products.Queries.GetListProductByCategoryAndIsDelete
{
	public class GetListProductByCategoryAndIsDeleteQuery : IRequest<List<GetListProductByCategoryAndIsDeleteViewModel>>
	{
		public int CategoryId { get; set; }
		public bool IsDeleted { get; set; }
		public class GetByIdProductQueryHandler : IRequestHandler<GetListProductByCategoryAndIsDeleteQuery, List<GetListProductByCategoryAndIsDeleteViewModel>>
		{
			private readonly IProductRepository _productRepository;
			private readonly IMapper _mapper;

			public GetByIdProductQueryHandler(IProductRepository productRepository, IMapper mapper)
			{
				_productRepository = productRepository;
				_mapper = mapper;
			}

			public async Task<List<GetListProductByCategoryAndIsDeleteViewModel>> Handle(GetListProductByCategoryAndIsDeleteQuery request, CancellationToken cancellationToken)
			{
				// sadece onaylanmış ürünler listelensin ama bakan kişi admin ise onaylanmamış ürünler de listelensin
				var products = await _productRepository.Query()
					.Where(p => p.CategoryId == request.CategoryId &&
								(request.IsDeleted == true
									? p.IsDeleted == true || p.IsDeleted == false
									: p.IsDeleted == false)
					)
					.Include(r => r.Reviews)!
						.ThenInclude(ru => ru.AppUser)
					.Include(u => u.User)
					.Include(c => c.Colors)
						.ThenInclude(c => c.Color)
					.Include(s => s.Sizes)
						.ThenInclude(s => s.Size)
					.Include(pi => pi.ProductImages)
					.ToListAsync(cancellationToken: cancellationToken);

				var mappedProduct = _mapper.Map<List<GetListProductByCategoryAndIsDeleteViewModel>>(products);

				foreach (var product in products)
				{
					if (product.Reviews == null) continue;
					foreach (var review in product.Reviews)
					{
						GetListReviewByProductViewModel reviewModel = new()
						{
							ReviewDescription = review.Description,
							ReviewRating = review.Rating,
							ReviewStatus = review.Status,
							ReviewCreatedDate = review.CreatedAt,
							ReviewUpdatedDate = review.UpdatedAt,
							Id = review.Id,
							User = new ReviewUser()
							{
								Id = review.AppUser.Id,
								Name = review.AppUser?.FirstName + " " + review.AppUser?.LastName,
								Image = review.AppUser?.ProfileImage
							}
						};
						mappedProduct[products.IndexOf(product)].ReviewsModel.Add(reviewModel);
					}
				}
				return mappedProduct;
			}
		}
	}
}
