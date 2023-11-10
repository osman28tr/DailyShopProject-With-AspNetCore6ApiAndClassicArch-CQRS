using AutoMapper;
using DailyShop.Business.Features.Products.Models;
using DailyShop.Business.Services.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyShop.Business.Features.Products.Queries.GetByIdProduct
{
    public class GetByIdProductQuery:IRequest<GetByIdProductViewModel>
    {
        public int ProductId { get; set; }
        public int CategoryId { get; set; }
        public bool IsDeleted { get; set; }
        public class GetByIdProductQueryHandler : IRequestHandler<GetByIdProductQuery, GetByIdProductViewModel>
        {
            private readonly IProductRepository _productRepository;
            private readonly IMapper _mapper;

            public GetByIdProductQueryHandler(IProductRepository productRepository, IMapper mapper)
            {
                _productRepository = productRepository;
                _mapper = mapper;
            }

            public async Task<GetByIdProductViewModel> Handle(GetByIdProductQuery request, CancellationToken cancellationToken)
            {
                var product = await _productRepository.Query().Where(p => p.Id == request.ProductId && p.IsDeleted == request.IsDeleted && p.CategoryId == request.CategoryId).Include(r => r.Reviews).ThenInclude(ru => ru.AppUser).Include(u => u.User).FirstOrDefaultAsync();

                var mappedProduct = _mapper.Map<GetByIdProductViewModel>(product);
                
                foreach (var review in product.Reviews)
                {
                    GetListReviewByProductViewModel reviewModel = new() { Name = review.Name, ReviewDescription = review.Description, ReviewRating = review.Rating, ReviewAvatar = review.Avatar, ReviewStatus = review.Status, ReviewCreatedDate = review.CreatedAt, ReviewUpdatedDate = review.UpdatedAt, UserName = review.AppUser.FirstName + " " + review.AppUser.LastName };
                    mappedProduct.ReviewsModel.Add(reviewModel);
                }
                return mappedProduct;
            }
        }
    }
}
