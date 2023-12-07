using AutoMapper;
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
            CreateMap<Address, AddressListByUserIdDto>()
                .ReverseMap();
            CreateMap<Address,UpdateAddressCommand>().ReverseMap();
            CreateMap<Address, UpdatedAddressDto>().ReverseMap();
            CreateMap<Address, AddressDto>().ReverseMap();
        }
    }
}
