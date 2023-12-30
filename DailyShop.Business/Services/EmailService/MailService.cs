using Core.CrossCuttingConcerns.Exceptions;
using DailyShop.Business.Services.AuthService;
using DailyShop.Business.Services.EmailService.Dtos;
using DailyShop.Business.Services.Repositories;
using MailKit.Net.Smtp;
using MimeKit;
using Org.BouncyCastle.Asn1.Ocsp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DailyShop.Business.Services.EmailService
{
    public class MailService : IMailService
    {
        private readonly IAppUserRepository _appUserRepository;
        public MailService(IAppUserRepository appUserRepository)
        {
            _appUserRepository = appUserRepository;
        }
        public async Task SendEmail(MailDto mail, CancellationToken cancellationToken)
        {
            MimeMessage email = new();
            email.From.Add(new MailboxAddress("DailyShop", "test12328t@gmail.com"));

            email.To.Add(new MailboxAddress("User", mail.ToEmail));

            email.Subject = mail.Subject;

            BodyBuilder bodyBuilder = new()
            {
                HtmlBody = mail.Body
            };

            email.Body = bodyBuilder.ToMessageBody();
            email.Body.Headers.Add("Content-Type", "text/html; charset=UTF-8");

            using SmtpClient smtp = new();
            await smtp.ConnectAsync("smtp.gmail.com", 587, cancellationToken: cancellationToken);
            await smtp.AuthenticateAsync("test12328t@gmail.com", "tdpbimjqzczhlgvc", cancellationToken);
            await smtp.SendAsync(email, cancellationToken);
            await smtp.DisconnectAsync(true, cancellationToken);
        }
    }
}
