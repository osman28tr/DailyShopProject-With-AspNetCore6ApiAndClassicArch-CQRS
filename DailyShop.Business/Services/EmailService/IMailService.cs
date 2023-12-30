using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DailyShop.Business.Services.EmailService.Dtos;

namespace DailyShop.Business.Services.EmailService
{
    public interface IMailService
    {
        Task SendEmail(MailDto mail, CancellationToken cancellationToken);
    }
}
