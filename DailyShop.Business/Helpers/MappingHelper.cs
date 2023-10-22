using AutoMapper;
using DailyShop.Business.Features.Auths.Dtos;
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
            CreateMap<AppUser, BaseUserDto>()
                .ForMember(c => c.FirstName, opt => opt.MapFrom(c => c.FirstName))
                .ForMember(c => c.LastName, opt => opt.MapFrom(c => c.LastName))
                .ForMember(c => c.Email, opt => opt.MapFrom(c => c.Email))
                .ForMember(c => c.PhoneNumber, opt => opt.MapFrom(c => c.PhoneNumber))
                .ForMember(c => c.ProfileImage, opt => opt.MapFrom(c => c.ProfileImage))
                .ReverseMap();
        }
    }
}
