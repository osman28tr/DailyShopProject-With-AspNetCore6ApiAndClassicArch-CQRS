using Core.Persistence.Repositories;
using DailyShop.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyShop.Business.Services.Repositories
{
    public interface IWebSiteSettingRepository:IAsyncRepository<WebSiteSetting>,IRepository<WebSiteSetting>
    {
    }
}
