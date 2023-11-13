using AutoMapper;
using Core.CrossCuttingConcerns.Exceptions;
using DailyShop.Business.Features.Products.Models;
using DailyShop.Business.Services.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DailyShop.Business.Features.Products.Queries.GetByIdProductDetail;

public class GetByIdProductDetailQuery : IRequest<GetByIdProductDetailViewModel>
{
	public int ProductId { get; set; }


	public class
		GetByIdProductDetailQueryHandler : IRequestHandler<GetByIdProductDetailQuery, GetByIdProductDetailViewModel>
	{
		private readonly IProductRepository _productRepository;
		private readonly IMapper _mapper;

		public GetByIdProductDetailQueryHandler(IProductRepository productRepository, IMapper mapper)
		{
			_productRepository = productRepository;
			_mapper = mapper;
		}

		public async Task<GetByIdProductDetailViewModel> Handle(GetByIdProductDetailQuery request,
			CancellationToken cancellationToken)
		{
			var product = await _productRepository.Query().Where(p => p.Id == request.ProductId).Include(r => r.Reviews)!.ThenInclude(ru => ru.AppUser).Include(u => u.User).Include(c => c.Colors).ThenInclude(c=>c.Color).Include(s => s.Sizes).Include(pi => pi.ProductImages).FirstOrDefaultAsync(cancellationToken: cancellationToken);

			if (product?.IsApproved == null || (bool) !product.IsApproved)
			{
				throw new BusinessException("Ürün onaylanmamıştır.");
			}
			var mappedProduct = _mapper.Map<GetByIdProductDetailViewModel>(product);

			if (product.Reviews == null) return mappedProduct;
			foreach (var review in product.Reviews)
			{
				GetListReviewByProductViewModel reviewModel = new()
				{
					Name = review.Name, ReviewDescription = review.Description, ReviewRating = review.Rating,
					ReviewAvatar = review.Avatar, ReviewStatus = review.Status,
					ReviewCreatedDate = review.CreatedAt, ReviewUpdatedDate = review.UpdatedAt,
					UserName = review.AppUser.FirstName + " " + review.AppUser.LastName
				};
				mappedProduct.ReviewsModel.Add(reviewModel);
			}

			return mappedProduct;
		}
	}


}