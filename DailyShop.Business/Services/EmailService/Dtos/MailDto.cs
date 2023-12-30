using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyShop.Business.Services.EmailService.Dtos
{
    public class MailDto
    {
        public string? Subject { get; set; }
        public string? Body { get; set; }
        public string? ToEmail { get; set; }

        public MailDto()
        {
        }

        public MailDto(string subject, string body,
                    string toEmail)
        {
            Subject = subject;
            ToEmail = toEmail;
            Body = body;
        }
    }
}
