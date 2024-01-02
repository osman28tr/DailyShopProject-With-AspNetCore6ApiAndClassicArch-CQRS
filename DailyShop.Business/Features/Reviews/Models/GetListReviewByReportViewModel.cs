using DailyShop.Business.Features.AppUsers.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyShop.Business.Features.Reviews.Models
{
    public class GetListReviewByReportViewModel
    {
        public GetListReviewByReportViewModel()
        {
            Review = new GetReviewByReportReviewViewModel();
        }
        public int Id { get; set; }
        public string ReportedMessage { get; set; }
        public DateTime CreatedAt { get; set; }
        public GetListUserDto ReporterUser { get; set; }
        public GetReviewByReportReviewViewModel Review { get; set; }
    }
}
