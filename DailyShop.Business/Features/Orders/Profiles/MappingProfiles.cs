using AutoMapper;
using DailyShop.Business.Features.Orders.Dtos;
using DailyShop.Business.Features.Orders.Models;
using DailyShop.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyShop.Business.Features.Orders.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Order, GetListOrderByUserIdViewModel>()
                .ForMember(x => x.GetListOrderItemByOrderViewModels, opt => opt.MapFrom(src => src.OrderItems))
                .ForMember(x => x.GetListOrderAddressByOrderViewModel, opt => opt.MapFrom(src => src.OrderAddress))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt))
                .ReverseMap();

            CreateMap<OrderItem, GetListOrderItemByOrderViewModel>()
                .ForMember(x => x.GetListProductByOrderViewModel, opt => opt.MapFrom(src => src.Product))
                .ReverseMap();
            CreateMap<Product, GetListProductByOrderViewModel>().ReverseMap();
            CreateMap<OrderAddress, GetListOrderAddressByOrderViewModel>().ReverseMap();

            CreateMap<Payment, InsertedPaymentDto>()
                .ReverseMap();
        }
    }
}
