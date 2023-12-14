using AutoMapper;
using Core.CrossCuttingConcerns.Exceptions;
using DailyShop.Business.Features.Auths.Dtos;
using DailyShop.Business.Services.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyShop.Business.Features.AppUsers.Queries.GetUser
{
    public class GetUserQuery : IRequest<LoggedUserDto>
    {
        public int UserId { get; set; }
        public class GetUserQueryHandler : IRequestHandler<GetUserQuery, LoggedUserDto>
        {
            private readonly IAppUserRepository _appUserRepository;
            private readonly IMapper _mapper;
            public GetUserQueryHandler(IAppUserRepository appUserRepository, IMapper mapper)
            {
                _appUserRepository = appUserRepository;
                _mapper = mapper;
            }
            public async Task<LoggedUserDto> Handle(GetUserQuery request, CancellationToken cancellationToken)
            {
                var user = await _appUserRepository.GetAsync(x => x.Id == request.UserId);
                var mappedUser = _mapper.Map<LoggedUserDto>(user);
                return mappedUser;
            }
        }
    }
}
