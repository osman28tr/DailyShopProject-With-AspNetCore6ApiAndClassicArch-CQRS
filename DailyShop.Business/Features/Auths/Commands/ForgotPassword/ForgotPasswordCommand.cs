using Core.CrossCuttingConcerns.Exceptions;
using DailyShop.Business.Services.AuthService;
using DailyShop.Business.Services.EmailService;
using DailyShop.Business.Services.EmailService.Dtos;
using DailyShop.Business.Services.Repositories;
using DailyShop.Entities.Concrete;
using MailKit.Net.Smtp;
using MediatR;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyShop.Business.Features.Auths.Commands.ForgotPassword
{
    public class ForgotPasswordCommand : IRequest
    {
        public string Email { get; set; }
        public class ForgotPasswordCommandHandler : IRequestHandler<ForgotPasswordCommand>
        {
            private readonly IAuthService _authService;
            private readonly IAppUserRepository _appUserRepository;
            private readonly IMailService _mailService;
            public ForgotPasswordCommandHandler(IAuthService authService, IAppUserRepository appUserRepository, IMailService mailService)
            {
                _authService = authService;
                _appUserRepository = appUserRepository;
                _mailService = mailService;
            }
            public async Task Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
            {
                var forgotUser = await _appUserRepository.GetAsync(x => x.Email == request.Email);
                if (forgotUser == null)
                    throw new BusinessException("Girdiğiniz mail'e ait bir kayıt bulunamadı. Kayıt olmadıysanız lütfen kayıt olunuz.");

                var forgotToken = await _authService.CreateAccessToken(forgotUser);
                var link = $"http://localhost:5173/forgot-password?token={forgotToken.Token}";

                await _mailService.SendEmail(new MailDto { ToEmail = request.Email, Subject = "Şifremi Unuttum", Body = $"Merhaba {forgotUser.FirstName} {forgotUser.LastName}, <br><br>Şifrenizi yenilemek için aşağıdaki linke tıklayınız. <br><a href='{link}'>Şifremi Yenile</a>" }, cancellationToken);
            }
        }
    }
}
