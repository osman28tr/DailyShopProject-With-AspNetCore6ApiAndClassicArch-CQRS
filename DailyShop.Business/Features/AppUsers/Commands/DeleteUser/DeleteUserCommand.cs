using DailyShop.Business.Features.AppUsers.Dtos;
using DailyShop.Business.Services.Repositories;
using DailyShop.Entities.Concrete;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyShop.Business.Features.AppUsers.Commands.DeleteUser
{
    public class DeleteUserCommand : IRequest<string>
    {
        public DeletedUserFrontendDto DeletedUserFrontendDto { get; set; }
        public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, string>
        {
            private readonly IAppUserRepository _appUserRepository;

            public DeleteUserCommandHandler(IAppUserRepository appUserRepository)
            {
                _appUserRepository = appUserRepository;
            }

            public async Task<string> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
            {
                AppUser deletedUser = await _appUserRepository.GetAsync(a => a.Id == request.DeletedUserFrontendDto.id);
                await _appUserRepository.DeleteAsync(deletedUser);
                return "Kullanıcı başarıyla silindi.";
            }
        }
    }
}
