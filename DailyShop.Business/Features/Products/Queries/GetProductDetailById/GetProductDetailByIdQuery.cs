using AutoMapper;
using Core.CrossCuttingConcerns.Exceptions;
using DailyShop.Business.Features.Products.Models;
using DailyShop.Business.Services.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DailyShop.Business.Features.Products.Queries.GetProductDetailById;

public class GetProductDetailByIdQuery : IRequest<GetProductDetailByIdViewModel>
{
    public int ProductId { get; set; }
    public int UserId { get; set; }

    public class
        GetByIdProductDetailQueryHandler : IRequestHandler<GetProductDetailByIdQuery, GetProductDetailByIdViewModel>
    {
        private readonly IProductRepository _productRepository;
        private readonly IAppUserRepository _appUserRepository;
        private readonly IMapper _mapper;

        public GetByIdProductDetailQueryHandler(IProductRepository productRepository,IAppUserRepository appUserRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _appUserRepository = appUserRepository;
            _mapper = mapper;
        }

        public async Task<GetProductDetailByIdViewModel> Handle(GetProductDetailByIdQuery request,
            CancellationToken cancellationToken)
        {
            var product = await _productRepository.Query().Where(p => p.Id == request.ProductId).Include(r => r.Reviews)!.ThenInclude(ru => ru.AppUser).Include(u => u.User).Include(c => c.Colors).ThenInclude(c => c.Color).Include(s => s.Sizes).ThenInclude(s => s.Size).Include(pi => pi.ProductImages).FirstOrDefaultAsync(cancellationToken: cancellationToken);

            var user = await _appUserRepository.Query().FirstOrDefaultAsync(x => x.Id == request.UserId);

            if ((product?.IsApproved == null || (bool)!product.IsApproved) && user.Role != "admin")
            {
                throw new BusinessException("Ürün onaylanmamıştır.");
            }

            if (product == null) throw new BusinessException("Ürün bulunamadı veya kaldırıldı.");
            var mappedProduct = _mapper.Map<GetProductDetailByIdViewModel>(product);

            if (product.Reviews == null) return mappedProduct;
            foreach (var review in product.Reviews)
            {
                GetListReviewByProductViewModel reviewModel = new()
                {
                    Name = review.Name,
                    ReviewDescription = review.Description,
                    ReviewRating = review.Rating,
                    ReviewAvatar = review.Avatar,
                    ReviewStatus = review.Status,
                    ReviewCreatedDate = review.CreatedAt,
                    ReviewUpdatedDate = review.UpdatedAt,
                    UserName = review.AppUser.FirstName + " " + review.AppUser.LastName
                };
                mappedProduct.ReviewsModel.Add(reviewModel);
            }

            return mappedProduct;
        }
    }


}