using Core.CrossCuttingConcerns.Exceptions;
using DailyShop.Business.Features.Reviews.Models;
using DailyShop.Business.Services.Repositories;
using DailyShop.Entities.Concrete;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyShop.Business.Features.Reviews.Queries.GetListReviewByProductId
{
    public class GetListReviewByProductIdQuery:IRequest<List<GetListReviewByProductIdViewModel>>
    {
        public int? ProductId { get; set; }
        public class GetListReviewByProductIdQueryHandler : IRequestHandler<GetListReviewByProductIdQuery, List<GetListReviewByProductIdViewModel>>
        {
            private readonly IReviewRepository _reviewRepository;
            private readonly IOrderRepository _orderRepository;
            public GetListReviewByProductIdQueryHandler(IReviewRepository reviewRepository, IOrderRepository orderRepository)
            {
                _reviewRepository = reviewRepository;
                _orderRepository = orderRepository;
            }
            public async Task<List<GetListReviewByProductIdViewModel>> Handle(GetListReviewByProductIdQuery request, CancellationToken cancellationToken)
            {
                var listReview = await _reviewRepository.Query().Where(x => x.ProductId == request.ProductId).Include(x=>x.AppUser).ToListAsync();
                var allOrder = await _orderRepository.Query().Include(x=>x.OrderItems).ToListAsync();
                if (listReview == null)
                    throw new BusinessException("Bu ürüne ait yorum bulunamadı.");
                List<GetListReviewByProductIdViewModel> mappedListReview = new List<GetListReviewByProductIdViewModel>();
                listReview.ForEach(x =>
                {
                    mappedListReview.Add(new GetListReviewByProductIdViewModel
                    {
                        Id = x.Id,
                        ReviewCreatedDate = DateTime.Now,
                        ReviewDescription = x.Description,
                        ReviewRating = x.Rating,
                        ReviewStatus = x.Status,
                        ReviewUpdatedDate = DateTime.Now,
                        User = new ReviewUser { Name = x.AppUser.FirstName + " " + x.AppUser.LastName, Email = x.AppUser.Email, Id = x.AppUser.Id, Image = x.AppUser.ProfileImage },
                        UserPurchasedThisProduct = allOrder.Any(y => y.OrderItems != null && y.UserId == x.AppUser.Id && y.OrderItems.Any(orderItem => orderItem.ProductId == request.ProductId))
                    });
                });
                return mappedListReview;
            }
        }
    }
}
