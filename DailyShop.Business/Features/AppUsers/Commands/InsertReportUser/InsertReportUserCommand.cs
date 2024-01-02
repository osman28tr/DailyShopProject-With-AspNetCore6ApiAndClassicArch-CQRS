using Core.CrossCuttingConcerns.Exceptions;
using DailyShop.Business.Features.AppUsers.Dtos;
using DailyShop.Business.Services.Repositories;
using DailyShop.Entities.Concrete;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyShop.Business.Features.AppUsers.Commands.InsertReportUser
{
    public class InsertReportUserCommand : IRequest
    {
        public int? UserId { get; set; }
        public int? ReporterUserId { get; set; }
        public ReportUserDto? ReportUserDto { get; set; }
        public class InsertReportUserCommandHandler : IRequestHandler<InsertReportUserCommand>
        {
            private readonly IAppUserRepository _appUserRepository;
            private readonly IReportUserRepository _reportUserRepository;
            public InsertReportUserCommandHandler(IAppUserRepository appUserRepository, IReportUserRepository reportUserRepository)
            {
                _appUserRepository = appUserRepository;
                _reportUserRepository = reportUserRepository;
            }
            public async Task Handle(InsertReportUserCommand request, CancellationToken cancellationToken)
            {
                var reportedUser = await _appUserRepository.GetAsync(x => x.Id == request.ReporterUserId);
                if (reportedUser == null)
                    throw new BusinessException("Raporlanmak istenen kullanıcı bulunamadı.");

                var IsReported = await _reportUserRepository.GetAsync(x => x.UserId == request.UserId && x.ReporterUserId == request.ReporterUserId);

                if (IsReported != null)
                    throw new BusinessException("Bu kullanıcıyı zaten raporladınız.");

                await _reportUserRepository.AddAsync(new ReportUser { ReportedMessage = request.ReportUserDto.ReportedMessage, UserId = request.UserId, ReporterUserId = request.ReporterUserId });
            }
        }
    }
}
