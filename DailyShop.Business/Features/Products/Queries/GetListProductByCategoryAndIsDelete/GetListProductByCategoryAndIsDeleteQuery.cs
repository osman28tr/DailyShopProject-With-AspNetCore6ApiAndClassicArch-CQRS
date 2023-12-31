using AutoMapper;
using DailyShop.Business.Features.Categories.Dtos;
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
        public List<GetListCategoryDto> GetListCategoryDtos { get; set; }
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

                var filteredCategory = request.GetListCategoryDtos.FirstOrDefault(x => x.Id == request.CategoryId);

                var allSubCategoryIds = GetAllSubCategoryIds(filteredCategory);

                List<Product> productList = new List<Product>();

                foreach (var categoryId in allSubCategoryIds)
                {
                    var products = await _productRepository.Query()
                    .Where(p => (user != null && user.Role == "admin") || p.IsApproved == true)
                    .Where(p => p.CategoryId == categoryId && (request.IsDeleted || p.IsDeleted == false))
                    .Include(r => r.Reviews)!
                    .ThenInclude(ru => ru.AppUser)
                    .ToListAsync(cancellationToken: cancellationToken);

                    productList.AddRange(products);
                }
                var mappedProducts = _mapper.Map<List<GetListProductByCategoryAndIsDeleteViewModel>>(productList);

                foreach (var mappedProduct in mappedProducts)
                {
                    var productColors = await _dpProductRepository.GetProductDetailColorByIdAsync(mappedProduct.Id);
                    foreach (var productColor in productColors)
                        mappedProduct.Colors?.Add(productColor);
                    var productSizes = await _dpProductRepository.GetProductDetailSizeByIdAsync(mappedProduct.Id);
                    foreach (var productSize in productSizes)
                        mappedProduct.Sizes?.Add(productSize);
                    var productImages = await _dpProductRepository.GetProductDetailImageByIdAsync(mappedProduct.Id);
                    foreach (var productImage in productImages)
                        mappedProduct.ProductImages?.Add(productImage);
                }
                foreach (var product in productList)
                {
                    if (product.Reviews == null) continue;
                    foreach (var review in product.Reviews)
                    {
                        GetListReviewByProductViewModel reviewModel = new()
                        {
                            ReviewRating = review.Rating,
                            ReviewStatus = review.Status,
                            ReviewCreatedDate = review.CreatedAt,
                            ReviewUpdatedDate = review.UpdatedAt,
                            Id = review.Id
                        };
                        mappedProducts[productList.IndexOf(product)].ReviewsModel?.Add(reviewModel);
                    }
                }
                return mappedProducts;
            }
            public static List<int> GetAllSubCategoryIds(GetListCategoryDto category)
            {
                var allIds = new List<int> { category.Id };

                foreach (var subCategory in category.SubCategories)
                {
                    var subCategoryIds = GetAllSubCategoryIds(subCategory);
                    allIds.AddRange(subCategoryIds);
                }

                return allIds;
            }
        }
    }
}