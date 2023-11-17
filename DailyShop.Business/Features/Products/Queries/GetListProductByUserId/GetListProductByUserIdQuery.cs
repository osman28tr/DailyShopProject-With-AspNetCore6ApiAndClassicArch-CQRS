using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Core.CrossCuttingConcerns.Exceptions;
using DailyShop.Business.Features.Products.Models;
using DailyShop.Business.Services.Repositories;
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
            private readonly IMapper _mapper;

            public GetProductByUserIdQueryHandler(IProductRepository productRepository, IMapper mapper)
            {
                _productRepository = productRepository;
                _mapper = mapper;
            }
            public async Task<List<GetListProductByUserIdViewModel>> Handle(GetListProductByUserIdQuery request, CancellationToken cancellationToken)
            {
                var products = await _productRepository.Query().Where(p => p.UserId == request.UserId)
                    .Include(u=>u.User)
                    .Include(c => c.ProductImages).Include(r => r.Reviews).ThenInclude(ap=>ap.AppUser).Include(c => c.Colors)
                    .ThenInclude(c => c.Color)
                    .Include(s => s.Sizes).ThenInclude(s => s.Size).ToListAsync();

                if (products == null) throw new BusinessException("Bu kullanıcıya ait bir ürün bulunamadı.");

                var mappedProducts = _mapper.Map<List<GetListProductByUserIdViewModel>>(products);

                foreach (var mappedProduct in mappedProducts)
                {
                    foreach (var product in products)
                    {
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
                    }
                }
                return mappedProducts;
            }
        }
    }
}
