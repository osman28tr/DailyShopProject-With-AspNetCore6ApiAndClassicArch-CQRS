using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.CrossCuttingConcerns.Exceptions;
using DailyShop.Business.Services.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DailyShop.Business.Features.Reviews.Commands.UpdateStatusReview
{
    public class UpdateStatusReviewCommand:IRequest
    {
        public int ReviewId { get; set; }
        public string Status { get; set; }
        public class UpdateStatusReviewCommandHandler:IRequestHandler<UpdateStatusReviewCommand>
        {
            private readonly IReviewRepository _reviewRepository;
            public UpdateStatusReviewCommandHandler(IReviewRepository reviewRepository)
            {
                _reviewRepository = reviewRepository;
            }
            public async Task Handle(UpdateStatusReviewCommand request, CancellationToken cancellationToken)
            {
                var review = await _reviewRepository.Query().FirstOrDefaultAsync(x => x.Id == request.ReviewId);
                if (review != null)
                {
                    review.Status = request.Status;
                    await _reviewRepository.UpdateAsync(review);
                    return;
                }
                throw new BusinessException("Yorum bulunamadı.");
            }
        }
    }
}
