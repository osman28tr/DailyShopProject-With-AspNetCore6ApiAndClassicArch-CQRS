using AutoMapper;
using DailyShop.Business.Features.Auths.DailyFrontends;
using DailyShop.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyShop.Business.Helpers
{
    public class MappingHelper:Profile
    {
        public MappingHelper()
        {
            CreateMap<AppUser, BaseUserFrontendDto>()
                .ForMember(c => c.name, opt => opt.MapFrom(c => c.FirstName))
                .ForMember(c => c.surname, opt => opt.MapFrom(c => c.LastName))
                .ForMember(c => c.email, opt => opt.MapFrom(c => c.Email))
                .ForMember(c => c.phone, opt => opt.MapFrom(c => c.PhoneNumber))
                .ForMember(c => c.profileImage, opt => opt.MapFrom(c => c.ProfileImage))
                .ReverseMap();
        }
    }
}
