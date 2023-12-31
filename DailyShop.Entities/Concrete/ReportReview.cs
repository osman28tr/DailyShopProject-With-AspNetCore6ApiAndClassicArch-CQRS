using Core.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyShop.Entities.Concrete
{
    public class ReportReview:Entity
    {
        public int? ReviewId { get; set; }
        public int? ReporterReviewId { get; set; }
        public string? ReportedMessage { get; set; }
        public Review? Review { get; set; }
        public Review? ReporterReview { get; set; }
        public ReportReview()
        {

        }
        public ReportReview(int id) : base(id)
        {
            Id = id;
        }
    }
}
