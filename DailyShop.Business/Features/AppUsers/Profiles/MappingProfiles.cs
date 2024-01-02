using AutoMapper;
using DailyShop.Business.Features.Addresses.Dtos;
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
            CreateMap<ReportUser, GetListUserByReportDto>()
                .ForMember(x => x.User, opt => opt.MapFrom(x => x.User))
                .ForPath(x => x.ReporterUser.Addresses, opt => opt.MapFrom(x => x.User.Addresses))
                .ForMember(x => x.ReporterUser, opt => opt.MapFrom(x => x.ReporterUser))
                .ForPath(x => x.User.Reviews, opt => opt.Ignore())
                .ForPath(x => x.ReporterUser.Reviews, opt => opt.Ignore())
                .ReverseMap();
            CreateMap<Address, AddressListByUserIdDto>()
                .ReverseMap();
        }
    }
}
