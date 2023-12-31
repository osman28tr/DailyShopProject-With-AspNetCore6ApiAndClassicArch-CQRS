using Core.Persistence.Repositories;
using DailyShop.Business.Services.Repositories;
using DailyShop.DataAccess.Concrete.EntityFramework.Contexts;
using DailyShop.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyShop.DataAccess.Concrete.EntityFramework.Repositories
{
    public class ReportUserRepository : EfRepositoryBase<ReportUser, DailyShopContext>, IReportUserRepository
    {
        public ReportUserRepository(DailyShopContext context) : base(context)
        {
        }
    }
}
