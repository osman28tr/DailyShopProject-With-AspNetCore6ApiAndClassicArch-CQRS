using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Core.CrossCuttingConcerns.Exceptions;
using DailyShop.Business.Features.Products.Models;
using DailyShop.Business.Services.Repositories;
using DailyShop.Business.Services.Repositories.Dapper;
using DailyShop.Entities.Concrete;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DailyShop.Business.Features.Products.Queries.GetListProductByUserId
{
    public class GetListProductByUserIdQuery: IRequest<List<GetListProductByUserIdViewModel>>
    {
        public int UserId { get; set; }
        public class GetProductByUserIdQueryHandler:IRequestHandler<GetListProductByUserIdQuery,List<GetListProductByUserIdViewModel>>
        {
            private readonly IProductRepository _productRepository;
            private readonly IDpProductRepository _dpProductRepository;
            private readonly IMapper _mapper;

            public GetProductByUserIdQueryHandler(IProductRepository productRepository,IDpProductRepository dpProductRepository, IMapper mapper)
            {
                _productRepository = productRepository;
                _dpProductRepository = dpProductRepository;
                _mapper = mapper;
            }
            public async Task<List<GetListProductByUserIdViewModel>> Handle(GetListProductByUserIdQuery request, CancellationToken cancellationToken)
            {
                var products = await _productRepository.Query().Where(p => p.UserId == request.UserId)
                    .Include(r => r.Reviews).ThenInclude(ap => ap.AppUser).ToListAsync();

                if (products == null) throw new BusinessException("Bu kullanıcıya ait bir ürün bulunamadı.");

                var mappedProducts = _mapper.Map<List<GetListProductByUserIdViewModel>>(products);
                foreach (var mappedProduct in mappedProducts)
                {
                    var productColors = await _dpProductRepository.GetProductDetailColorByIdAsync(mappedProduct.Id);
                    if (productColors != null)
                    {
                        foreach (var productColor in productColors)
                        {
                            mappedProduct.Colors.Add(productColor);
                        }
                    }
                    var productSizes = await _dpProductRepository.GetProductDetailSizeByIdAsync(mappedProduct.Id);
                    if (productSizes != null)
                    {
                        foreach (var productSize in productSizes)
                        {
                            mappedProduct.Sizes.Add(productSize);
                        }
                    }
                    var productImages = await _dpProductRepository.GetProductDetailImageByIdAsync(mappedProduct.Id);
                    if (productImages != null)
                    {
                        foreach (var productImage in productImages)
                        {
                            mappedProduct.ProductImages.Add(productImage);
                        }
                    }
                }
                foreach (var mappedProduct in mappedProducts)
                {
                    foreach (var product in products)
                    {
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
                                    Name = review.AppUser.FirstName + " " + review.AppUser.LastName,
                                    Image = review.AppUser.ProfileImage
                                }
                            };
                            mappedProduct.ReviewsModel.Add(reviewModel);
                        }
                    }
                }
                return mappedProducts;
            }
        }
    }
}
