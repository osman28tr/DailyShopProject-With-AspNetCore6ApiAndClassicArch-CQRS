using AutoMapper;
using DailyShop.Business.Features.AppUsers.Dtos;
using DailyShop.Business.Features.Auths.Dtos;
using DailyShop.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyShop.Business.Features.AppUsers.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<AppUser, GetListUserDto>()
                .ForMember(x => x.Reviews, opt => opt.Ignore())
                .ReverseMap();
            CreateMap<AppUser, LoggedUserDto>()
                .ForMember(x => x.Addresses, opt => opt.MapFrom(x => x.Addresses));
            CreateMap<AppUser, UpdatedUserDto>().ReverseMap();
        }
    }
}
