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
                    .Include(u => u.User)
                    .Include(c => c.ProductImages).Include(r => r.Reviews).ThenInclude(ap => ap.AppUser).Include(c => c.Colors)
                    .ThenInclude(c => c.Color)
                    .Include(s => s.Sizes).ThenInclude(s => s.Size).ToListAsync();

                //var products = await _productRepository.Query().Where(p => p.UserId == request.UserId)
                //    .Include(u => u.User).ToListAsync();

                //var productImages = products.AsQueryable().Include(x => x.ProductImages).LoadAsync();
                //var reviews = products.AsQueryable().Include(x => x.Reviews).LoadAsync();
                //var colors = products.AsQueryable().Include(x=>x.Colors).ThenInclude(x=>x.Color).LoadAsync();
                //var sizes = products.AsQueryable().Include(x => x.Sizes).ThenInclude(x => x.Size).LoadAsync();


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
