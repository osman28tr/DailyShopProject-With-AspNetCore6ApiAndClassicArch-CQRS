using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyShop.Business.Features.AppUsers.Dtos
{
    public class GetListUserByReportDto
    {
        public int Id { get; set; }
        public GetListUserDto User { get; set; }
        public GetListUserDto ReporterUser { get; set; }
        public string ReportedMessage { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
