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
    public class GetListUserByReportQuery:IRequest<List<GetListUserDto>>
    {
        public class GetListUserByReportQueryHandler : IRequestHandler<GetListUserByReportQuery, List<GetListUserDto>>
        {
            private readonly IAppUserRepository _appUserRepository;
            private readonly IMapper _mapper;
            public GetListUserByReportQueryHandler(IAppUserRepository appUserRepository, IMapper mapper)
            {
                _appUserRepository = appUserRepository;
                _mapper = mapper;
            }
            public async Task<List<GetListUserDto>> Handle(GetListUserByReportQuery request, CancellationToken cancellationToken)
            {
                var reportedUsers = await _appUserRepository.Query().Where(x => x.Status == false).ToListAsync();
                if (reportedUsers.Count == 0)
                    throw new BusinessException("Raporlanan kullanıcı bulunamadı.");
                var mappedReportUsers = _mapper.Map<List<GetListUserDto>>(reportedUsers);
                return mappedReportUsers;
            }
        }
    }
}
