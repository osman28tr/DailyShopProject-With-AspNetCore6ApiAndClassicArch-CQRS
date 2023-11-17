using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DailyShop.Business.Features.Carts.Models;
using DailyShop.Entities.Concrete;

namespace DailyShop.Business.Features.Carts.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Cart, GetListCartByUserViewModel>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.CartItems.Select(x => x.Product.Name)))
                .ForMember(dest => dest.ProductPrice, opt => opt.MapFrom(src => src.CartItems.Select(x => x.Product.Price)))
                .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.CartItems.Select(x => x.Quantity)))
                .ForMember(dest => dest.TotalPrice, opt => opt.MapFrom(src => src.CartItems.Select(x => x.TotalPrice)))
                .ReverseMap();
        }
    }
}
