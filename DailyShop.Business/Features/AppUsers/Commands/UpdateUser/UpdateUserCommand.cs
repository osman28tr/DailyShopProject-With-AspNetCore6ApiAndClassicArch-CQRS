using AutoMapper;
using DailyShop.Business.Features.Auths.DailyFrontends;
using DailyShop.Business.Services.Repositories;
using DailyShop.Entities.Concrete;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyShop.Business.Features.AppUsers.Commands.UpdateUser
{
    public class UpdateUserCommand : IRequest<int>
    {
        public UpdatedUserFrontendDto UpdatedUserFrontendDto { get; set; }
        public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, int>
        {
            private readonly IAppUserRepository _appUserRepository;
            private readonly IMapper _mapper;

            public UpdateUserCommandHandler(IAppUserRepository appUserRepository, IMapper mapper)
            {
                _appUserRepository = appUserRepository;
                _mapper = mapper;
            }

            public async Task<int> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
            {
                AppUser newUser = new()
                {
                    FirstName = request.UpdatedUserFrontendDto.FirstName,
                    LastName = request.UpdatedUserFrontendDto.LastName,
                    Email = request.UpdatedUserFrontendDto.Email,
                    PhoneNumber = request.UpdatedUserFrontendDto.PhoneNumber,
                    ProfileImage = request.UpdatedUserFrontendDto.ProfileImage,
                };
                AppUser oldUser = await _appUserRepository.GetAsync(a => a.Id == request.UpdatedUserFrontendDto.id);
                oldUser.FirstName = newUser.FirstName;
                oldUser.LastName = newUser.LastName;
                oldUser.Email = newUser.Email;
                oldUser.PhoneNumber = newUser.PhoneNumber;
                oldUser.ProfileImage = newUser.ProfileImage;
                await _appUserRepository.UpdateAsync(oldUser);
                return oldUser.Id;
            }
        }
    }
}
