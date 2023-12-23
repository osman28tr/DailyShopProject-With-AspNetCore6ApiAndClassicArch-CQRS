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
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<WebSiteSetting, UpdatedWebSiteSettingDto>()
                .ForMember(dest => dest.About, opt => opt.MapFrom(src => src.HtmlContent))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Adres))
                .ForMember(dest => dest.SiteIcon, opt => opt.Ignore())
                .ReverseMap();
            CreateMap<WebSiteSetting, WebSiteSettingDto>()
                 .ForMember(dest => dest.About, opt => opt.MapFrom(src => src.HtmlContent))
                 .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                 .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.Phone))
                 .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Adres))
                 .ForMember(dest => dest.SiteIcon, opt => opt.MapFrom(src => src.Icon))
                .ReverseMap();

        }
    }
}
