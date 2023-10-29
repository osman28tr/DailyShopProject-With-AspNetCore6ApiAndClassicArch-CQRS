using AutoMapper;
using DailyShop.Business.Features.WebSiteSettings.Dtos;
using DailyShop.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyShop.Business.Features.WebSiteSettings.Profiles
{
    public class MappingProfiles:Profile
    {
        public MappingProfiles()
        {
            CreateMap<WebSiteSetting, UpdatedWebSiteSettingDto>().ReverseMap();
        }
    }
}
