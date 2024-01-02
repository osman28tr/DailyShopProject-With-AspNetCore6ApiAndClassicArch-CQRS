using Core.CrossCuttingConcerns.Exceptions;
using DailyShop.Business.Services.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyShop.Business.Features.AppUsers.Commands.DeleteReportUser
{
    public class DeleteReportUserCommand:IRequest
    {
        public int ReportUserId { get; set; }
        public class DeleteReportUserCommandHandler : IRequestHandler<DeleteReportUserCommand>
        {
            private readonly IReportUserRepository _reportUserRepository;
            public DeleteReportUserCommandHandler(IReportUserRepository reportUserRepository)
            {
                _reportUserRepository = reportUserRepository;
            }
            public async Task Handle(DeleteReportUserCommand request, CancellationToken cancellationToken)
            {
                var reportUser = await _reportUserRepository.GetAsync(x => x.Id == request.ReportUserId);
                if (reportUser == null)
                    throw new BusinessException("Rapor bulunamadı.");
                await _reportUserRepository.DeleteAsync(reportUser, false);
            }
        }
    }
}
