using Core.CrossCuttingConcerns.Exceptions;
using DailyShop.Business.Services.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyShop.Business.Features.Reviews.Commands.DeleteReportReview
{
    public class DeleteReportReviewCommand : IRequest
    {
        public int ReportId { get; set; }
        public class DeleteReportReviewCommandHandler : IRequestHandler<DeleteReportReviewCommand>
        {
            private readonly IReportReviewRepository _reportReviewRepository;
            public DeleteReportReviewCommandHandler(IReportReviewRepository reportReviewRepository)
            {
                _reportReviewRepository = reportReviewRepository;
            }
            public async Task Handle(DeleteReportReviewCommand request, CancellationToken cancellationToken)
            {
                var reportReview = await _reportReviewRepository.GetAsync(x => x.Id == request.ReportId);
                if (reportReview == null)
                    throw new BusinessException("Yoruma ait rapor bulunamadı.");
                await _reportReviewRepository.DeleteAsync(reportReview, false);
            }
        }
    }
}
