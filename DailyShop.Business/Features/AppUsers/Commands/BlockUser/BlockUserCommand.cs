using DailyShop.Business.Features.AppUsers.Dtos;
using DailyShop.Business.Services.Repositories;
using DailyShop.Entities.Concrete;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyShop.Business.Features.AppUsers.Commands.BlockUser
{
    public class BlockUserCommand : IRequest<string>
    {
        public BlockedUserDto BlockedUserDto { get; set; }
        public class DeleteUserCommandHandler : IRequestHandler<BlockUserCommand, string>
        {
            private readonly IAppUserRepository _appUserRepository;

            public DeleteUserCommandHandler(IAppUserRepository appUserRepository)
            {
                _appUserRepository = appUserRepository;
            }

            public async Task<string> Handle(BlockUserCommand request, CancellationToken cancellationToken)
            {
                AppUser blockedUser = await _appUserRepository.GetAsync(a => a.Id == request.BlockedUserDto.Id);
                if (blockedUser.Status == true)
                    blockedUser.Status = false;
                else
                    blockedUser.Status = true;
                await _appUserRepository.UpdateAsync(blockedUser);
                return "Kullanıcı başarıyla engellendi.";
            }
        }
    }
}
