using DailyShop.Business.Features.Reviews.Dtos;
using DailyShop.Business.Services.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyShop.Business.Features.Reviews.Commands.ReportReview
{
    public class ReportReviewCommand:IRequest
    {
        public int? ReviewId { get; set; }
        public ReportReviewDto? MyProperty { get; set; }
        public class ReportReviewCommandHandler : IRequestHandler<ReportReviewCommand>
        {
            private readonly IReviewRepository _reviewRepository;
            public ReportReviewCommandHandler(IReviewRepository reviewRepository)
            {
                _reviewRepository = reviewRepository;
            }
            public async Task Handle(ReportReviewCommand request, CancellationToken cancellationToken)
            {
                var review = await _reviewRepository.GetAsync(x => x.Id == request.ReviewId);
                review.Status = "Reject";
                review.UpdatedAt = DateTime.Now;
                await _reviewRepository.UpdateAsync(review);
            }
        }
    }
}
