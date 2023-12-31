using Core.CrossCuttingConcerns.Exceptions;
using DailyShop.Business.Features.Reviews.Dtos;
using DailyShop.Business.Services.Repositories;
using DailyShop.Entities.Concrete;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyShop.Business.Features.Reviews.Commands.InsertReportReview
{
    public class ReportReviewCommand:IRequest
    {
        public int? ReviewId { get; set; }
        public int? ReporterUserId { get; set; }
        public ReportReviewDto? ReportReviewDto { get; set; }
        public class ReportReviewCommandHandler : IRequestHandler<ReportReviewCommand>
        {
            private readonly IReportReviewRepository _reportReviewRepository;
            private readonly IReviewRepository _reviewRepository;
            public ReportReviewCommandHandler(IReportReviewRepository reportReviewRepository, IReviewRepository reviewRepository)
            {
                _reportReviewRepository = reportReviewRepository;
                _reviewRepository = reviewRepository;
            }
            public async Task Handle(ReportReviewCommand request, CancellationToken cancellationToken)
            {
                var review = await _reviewRepository.GetAsync(x => x.Id == request.ReviewId);
                if (review == null)
                    throw new BusinessException("Yorum bulunamadı.");

                await _reportReviewRepository.AddAsync(new ReportReview { ReportedMessage = request.ReportReviewDto.ReportedMessage, ReviewId = request.ReviewId, ReporterUserId = request.ReporterUserId });
            }
        }
    }
}
