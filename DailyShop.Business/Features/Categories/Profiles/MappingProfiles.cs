using AutoMapper;
using DailyShop.Business.Features.Categories.DailyFrontendDtos;
using DailyShop.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyShop.Business.Features.Categories.Profiles
{
    public class MappingProfiles:Profile
    {
        public MappingProfiles()
        {
            CreateMap<Category,GetListCategoryFrontendDto>()
                .ForMember(c => c.name, opt => opt.MapFrom(c => c.Name))
                .ReverseMap();
        }
    }
}
