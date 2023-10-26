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
    public class UpdateUserCommand : IRequest<UpdatedUserDto>
    {
        public UpdatedUserDto UpdatedUserDto { get; set; }
        public int Id { get; set; }
        public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, UpdatedUserDto>
        {
            private readonly IAppUserRepository _appUserRepository;
            private readonly IMapper _mapper;

            public UpdateUserCommandHandler(IAppUserRepository appUserRepository, IMapper mapper)
            {
                _appUserRepository = appUserRepository;
                _mapper = mapper;
            }

            public async Task<UpdatedUserDto> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
            {
                AppUser oldUser = await _appUserRepository.GetAsync(a => a.Id == request.Id);

                oldUser.FirstName = request.UpdatedUserDto.FirstName;
                oldUser.LastName = request.UpdatedUserDto.LastName;
                oldUser.Email = request.UpdatedUserDto.Email;
                oldUser.PhoneNumber = request.UpdatedUserDto.PhoneNumber;
                oldUser.ProfileImage = request.UpdatedUserDto.ProfileImage;

                AppUser updatedUser = await _appUserRepository.UpdateAsync(oldUser);
                var mappedUserDto = _mapper.Map<UpdatedUserDto>(updatedUser);
                return mappedUserDto;
            }
        }
    }
}
