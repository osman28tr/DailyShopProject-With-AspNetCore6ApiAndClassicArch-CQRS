using AutoMapper;
using DailyShop.Business.Features.Auths.Dtos;
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
        public UpdatedUserDto UpdatedUserDto { get; set; }
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
                    FirstName = request.UpdatedUserDto.FirstName,
                    LastName = request.UpdatedUserDto.LastName,
                    Email = request.UpdatedUserDto.Email,
                    PhoneNumber = request.UpdatedUserDto.PhoneNumber,
                    ProfileImage = request.UpdatedUserDto.ProfileImage,
                };
                AppUser oldUser = await _appUserRepository.GetAsync(a => a.Id == request.UpdatedUserDto.Id);
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
