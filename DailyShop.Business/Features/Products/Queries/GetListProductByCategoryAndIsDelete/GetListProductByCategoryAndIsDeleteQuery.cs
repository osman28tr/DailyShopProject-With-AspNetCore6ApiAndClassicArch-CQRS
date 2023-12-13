using AutoMapper;
using DailyShop.Business.Features.Products.Models;
using DailyShop.Business.Services.Repositories;
using DailyShop.Business.Services.Repositories.Dapper;
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
        public int UserId { get; set; }
        public bool IsDeleted { get; set; }
        public class GetByIdProductQueryHandler : IRequestHandler<GetListProductByCategoryAndIsDeleteQuery, List<GetListProductByCategoryAndIsDeleteViewModel>>
        {
            private readonly IProductRepository _productRepository;
            private readonly IAppUserRepository _appUserRepository;
            private readonly IDpProductRepository _dpProductRepository;
            private readonly IMapper _mapper;

            public GetByIdProductQueryHandler(IProductRepository productRepository, IDpProductRepository dpProductRepository, IAppUserRepository appUserRepository, IMapper mapper)
            {
                _productRepository = productRepository;
                _dpProductRepository = dpProductRepository;
                _appUserRepository = appUserRepository;
                _mapper = mapper;
            }

            public async Task<List<GetListProductByCategoryAndIsDeleteViewModel>> Handle(GetListProductByCategoryAndIsDeleteQuery request, CancellationToken cancellationToken)
            {
                // sadece onaylanmış ürünler listelensin ama bakan kişi admin ise onaylanmamış ürünler de listelensin
                var user = await _appUserRepository.Query().FirstOrDefaultAsync(x => x.Id == request.UserId);

                List<Product> products = new List<Product>();
                if (user.Role == "admin")
                {
                    products = await _productRepository.Query()
                        .Where(p => p.CategoryId == request.CategoryId
                        )
                        .Include(r => r.Reviews)!
                        .ThenInclude(ru => ru.AppUser)
                        .ToListAsync(cancellationToken: cancellationToken);
                }
                else
                {
                    products = await _productRepository.Query()
                        .Where(p => p.CategoryId == request.CategoryId
                        && p.IsDeleted == false)
                        .Include(r => r.Reviews)!
                        .ThenInclude(ru => ru.AppUser)
                        .ToListAsync(cancellationToken: cancellationToken);
                }

                var mappedProducts = _mapper.Map<List<GetListProductByCategoryAndIsDeleteViewModel>>(products);

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
                        mappedProducts[products.IndexOf(product)].ReviewsModel.Add(reviewModel);
                    }
                }
                return mappedProducts;
            }
        }
    }
}
