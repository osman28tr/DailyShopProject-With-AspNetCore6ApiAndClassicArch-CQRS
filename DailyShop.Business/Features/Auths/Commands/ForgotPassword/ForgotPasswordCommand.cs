using Core.CrossCuttingConcerns.Exceptions;
using Core.Mailing;
using DailyShop.Business.Services.AuthService;
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
            public ForgotPasswordCommandHandler(IAuthService authService, IAppUserRepository appUserRepository)
            {
                _authService = authService;
                _appUserRepository = appUserRepository;
            }
            public async Task Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
            {
                MimeMessage email = new();

                email.From.Add(new MailboxAddress("DailyShop Admin", "test12328t@gmail.com"));

                email.To.Add(new MailboxAddress("User", request.Email));

                email.Subject = "Şifremi Unuttum";

                AppUser forgotUser = await _appUserRepository.GetAsync(x => x.Email == request.Email);
                if (forgotUser == null)
                    throw new BusinessException("Girdiğiniz mail'e ait bir kayıt bulunamadı. Kayıt olmadıysanız lütfen kayıt olunuz.");

                var forgotToken = await _authService.CreateAccessToken(forgotUser);

                BodyBuilder bodyBuilder = new()
                {
                    TextBody = $"{forgotToken.Token} \n Lütfen bu token'ı girerek sistemde şifrenizi değiştiriniz.",
                };

                email.Body = bodyBuilder.ToMessageBody();

                using SmtpClient smtp = new();
                smtp.Connect("smtp.gmail.com", 587);
                smtp.Authenticate("test12328t@gmail.com", "tdpbimjqzczhlgvc"); // şifre için google hesap'da iki adımlı doğrulama açılması gerek, ondan sonra aramada uygulama şifreleri yaz çıkan ekranda uygulama ismini yaz oluştura bas oluşan şifreyi buraya yaz.
                //smtp.Authenticate(_mailSettings.UserName, _mailSettings.Password);
                smtp.Send(email);
                smtp.Disconnect(true);

            }
        }
    }
}
