using Core.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyShop.Entities.Concrete
{
    public class ReportUser:Entity
    {
        public int? UserId { get; set; }
        public int? ReporterUserId { get; set; }
        public string? ReportedMessage { get; set; }
        public AppUser? User { get; set; }
        public AppUser? ReporterUser { get; set; }
        public ReportUser()
        {
            
        }
        public ReportUser(int id):base(id)
        {
            Id = id;
        }
    }
}
