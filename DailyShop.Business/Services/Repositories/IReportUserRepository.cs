using Core.Persistence.Repositories;
using DailyShop.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyShop.Business.Services.Repositories
{
    public interface IReportUserRepository:IAsyncRepository<ReportUser>,IRepository<ReportUser>
    {
    }
}
