using Core.CrossCuttingConcerns.Exceptions;
using DailyShop.Business.Services.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyShop.Business.Features.Reviews.Commands.DeleteReview
{
    public class DeleteReviewCommand : IRequest
    {
        public int? ReviewId { get; set; }
        public class DeleteReviewCommandHandler : IRequestHandler<DeleteReviewCommand>
        {
            private readonly IReviewRepository _reviewRepository;
            public DeleteReviewCommandHandler(IReviewRepository reviewRepository)
            {
                _reviewRepository = reviewRepository;
            }
            public async Task Handle(DeleteReviewCommand request, CancellationToken cancellationToken)
            {
                var review = await _reviewRepository.GetAsync(x => x.Id == request.ReviewId);
                if (review == null)
                    throw new BusinessException("Yorum bulunamadı.");
                await _reviewRepository.DeleteAsync(review, false);
            }
        }
    }
}
