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
    public class WebSiteSettingRepository : EfRepositoryBase<WebSiteSetting, DailyShopContext>, IWebSiteSettingRepository
    {
        public WebSiteSettingRepository(DailyShopContext context) : base(context)
        {
        }
    }
}
