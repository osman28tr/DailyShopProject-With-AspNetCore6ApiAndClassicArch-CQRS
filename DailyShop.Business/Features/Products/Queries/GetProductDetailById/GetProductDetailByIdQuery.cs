using AutoMapper;
using Core.CrossCuttingConcerns.Exceptions;
using DailyShop.Business.Features.Products.Models;
using DailyShop.Business.Services.Repositories;
using DailyShop.Business.Services.Repositories.Dapper;
using DailyShop.Entities.Concrete;
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
        private readonly IDpProductRepository _dpProductRepository;
        private readonly IDpOrderRepository _dpOrderRepository;
        private readonly IAppUserRepository _appUserRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public GetByIdProductDetailQueryHandler(IProductRepository productRepository, IDpProductRepository dpProductRepository, IDpOrderRepository dpOrderRepository, IAppUserRepository appUserRepository, IOrderRepository orderRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _appUserRepository = appUserRepository;
            _dpProductRepository = dpProductRepository;
            _dpOrderRepository = dpOrderRepository;
            _orderRepository = orderRepository;
            _mapper = mapper;
        }
            public async Task<GetProductDetailByIdViewModel> Handle(GetProductDetailByIdQuery request,
            CancellationToken cancellationToken)
            {
                var product = await _productRepository.Query().Where(p => p.Id == request.ProductId).FirstOrDefaultAsync(cancellationToken: cancellationToken);

                var category = await _dpProductRepository.GetProductDetailCategoryByIdAsync(product.CategoryId);
                var colors = await _dpProductRepository.GetProductDetailColorByIdAsync(request.ProductId);
                var sizes = await _dpProductRepository.GetProductDetailSizeByIdAsync(request.ProductId);
                var productImages = await _dpProductRepository.GetProductDetailImageByIdAsync(request.ProductId);
                var productUserName = await _dpProductRepository.GetProductDetailUserByIdAsync(request.ProductId);

                var user = await _appUserRepository.Query().FirstOrDefaultAsync(x => x.Id == request.UserId, cancellationToken: cancellationToken);

                if (product?.IsApproved == null || (bool)!product.IsApproved || product.IsDeleted == true)
                {
                    if ((user != null && user.Role != "admin") || user == null)
                    {
                        throw new BusinessException("Ürün onaylanmamıştır.");
                    }
                }

                if (product == null) throw new BusinessException("Ürün bulunamadı veya kaldırıldı.");

                var mappedProduct = _mapper.Map<GetProductDetailByIdViewModel>(product);

                Category categoryInProduct = new() { Name = category.Name, Id = category.Id, ParentCategoryId = category.ParentCategoryId };

                var mappedCategory = _mapper.Map<GetCategoryAtGetProductDetail>(categoryInProduct);
                mappedProduct.Category = mappedCategory;

                mappedProduct.UserName = productUserName;
                mappedProduct.UserId = product.UserId;

                if (colors.Any())
                    foreach (var color in colors)
                        mappedProduct.Colors?.Add(color);

                if (sizes.Any())
                    foreach (var size in sizes)
                        mappedProduct.Sizes?.Add(size);

                if (productImages.Any())
                    foreach (var productImage in productImages)
                        mappedProduct.ProductImages?.Add(productImage);

                return mappedProduct;
            }
        }
        //public List<GetListReviewByProductViewModel> MapReviews(Product product, List<Order> allOrder, ICollection<Review> reviews)
        //{
        //    List<GetListReviewByProductViewModel> mappedReviews = new List<GetListReviewByProductViewModel>();

        //    foreach (var review in reviews.Where(r => r.ProductId == product.Id && r.ParentReviewId == null))
        //    {
        //        GetListReviewByProductViewModel reviewModel = MapReviewViewModel(review, allOrder, product);
        //        mappedReviews.Add(reviewModel);

        //        // Eğer review'ın altında subreviews varsa, bunları da dönmek için rekürsif fonksiyonu çağırın
        //        MapSubReviews(product, allOrder, reviews, review, mappedReviews);
        //    }

        //    return mappedReviews;
        //}

        //private void MapSubReviews(Product product, List<Order> allOrder, ICollection<Review> reviews, Review parentReview, List<GetListReviewByProductViewModel> mappedReviews)
        //{
        //    foreach (var subReview in reviews.Where(r => r.ParentReviewId == parentReview.Id))
        //    {
        //        GetListReviewByProductViewModel subReviewModel = MapReviewViewModel(subReview, allOrder, product);

        //        // Eğer subreview'ın altında başka subreviews varsa, bunları da dönmek için rekürsif fonksiyonu çağırın
        //        MapSubReviews(product, allOrder, reviews, subReview, subReviewModel.SubReviews ??= new List<GetListReviewByProductViewModel>());

        //        // Ana review'in altındaki subreviews koleksiyonuna ekle
        //        mappedReviews.Add(subReviewModel);
        //    }
        //}
        //private GetListReviewByProductViewModel MapReviewViewModel(Review review, List<Order> allOrder, Product product)
        //{
        //    // Yukarıdaki kodunuzdan alarak güncelleyin
        //    // ...
        //    GetListReviewByProductViewModel reviewModel = new()
        //    {
        //        ReviewDescription = review.Description,
        //        ReviewRating = review.Rating,
        //        ReviewStatus = review.Status,
        //        ReviewCreatedDate = review.CreatedAt,
        //        ReviewUpdatedDate = review.UpdatedAt,
        //        ParentReviewId = review.ParentReviewId,
        //        Id = review.Id,
        //        User = new ReviewUser()
        //        {
        //            Id = review.AppUser.Id,
        //            Name = review.AppUser.FirstName + " " + review.AppUser.LastName,
        //            Image = review.AppUser.ProfileImage,
        //            Email = review.AppUser.Email,
        //        },
        //        UserPurchasedThisProduct = allOrder.Any(x => x.OrderItems != null && x.UserId == review.AppUser.Id && x.OrderItems.Any(orderItem => orderItem.ProductId == product.Id))
        //    };
        //    return reviewModel;
        //}
    }