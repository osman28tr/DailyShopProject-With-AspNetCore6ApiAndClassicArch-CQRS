using AutoMapper;
using Core.CrossCuttingConcerns.Exceptions;
using DailyShop.Business.Features.AppUsers.Dtos;
using DailyShop.Business.Services.Repositories;
using DailyShop.Entities.Concrete;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyShop.Business.Features.AppUsers.Queries.GetListUserByReport
{
    public class GetListUserByReportQuery:IRequest<List<GetListUserByReportDto>>
    {
        public class GetListUserByReportQueryHandler : IRequestHandler<GetListUserByReportQuery, List<GetListUserByReportDto>>
        {
            private readonly IReportUserRepository _reportUserRepository;
            private readonly IAppUserRepository _appUserRepository;
            private readonly IMapper _mapper;
            public GetListUserByReportQueryHandler(IMapper mapper, IReportUserRepository reportUserRepository)
            {
                _mapper = mapper;
                _reportUserRepository = reportUserRepository;
            }
            public async Task<List<GetListUserByReportDto>> Handle(GetListUserByReportQuery request, CancellationToken cancellationToken)
            {
                var reportedUsers = await _reportUserRepository.Query().Include(x => x.User).Include(x => x.ReporterUser).ToListAsync();
                if (reportedUsers == null)
                    throw new BusinessException("Raporlanan kullanıcı bulunamadı.");
                var mappedReportedUsers = _mapper.Map<List<GetListUserByReportDto>>(reportedUsers);
                return mappedReportedUsers;
            }
        }
    }
}
