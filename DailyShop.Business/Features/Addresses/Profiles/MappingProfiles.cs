using AutoMapper;
using DailyShop.Business.Features.Addresses.Commands.InsertAddress;
using DailyShop.Business.Features.Addresses.Commands.UpdateAddress;
using DailyShop.Business.Features.Addresses.Dtos;
using DailyShop.Business.Features.Auths.Dtos;
using DailyShop.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyShop.Business.Features.Addresses.Profiles
{
	public class MappingProfiles:Profile
	{
        public MappingProfiles()
        {
            //CreateMap<List<Address>,List<AddressListByUserIdDto>>().ReverseMap();
            CreateMap<Address, AddressListByUserIdDto>().ForMember(c => c.FirstName, opt => opt.MapFrom(c => c.AppUser.FirstName))
                .ForMember(c => c.LastName, opt => opt.MapFrom(c => c.AppUser.LastName))
                .ForMember(c => c.Email, opt => opt.MapFrom(c => c.AppUser.Email))
                .ForMember(c => c.PhoneNumber, opt => opt.MapFrom(c => c.AppUser.PhoneNumber))
                .ForMember(c => c.ProfileImage, opt => opt.MapFrom(c => c.AppUser.ProfileImage))
                .ReverseMap();
            CreateMap<Address, DeletedAddressDto>().ReverseMap();
            CreateMap<Address, InsertAddressCommand>().ReverseMap();
            CreateMap<Address, InsertedAddressDto>().ReverseMap();
            CreateMap<Address,UpdateAddressCommand>().ReverseMap();
            CreateMap<Address, UpdatedAddressDto>().ReverseMap();
            CreateMap<Address, AddressDto>().ReverseMap();
        }
    }
}
