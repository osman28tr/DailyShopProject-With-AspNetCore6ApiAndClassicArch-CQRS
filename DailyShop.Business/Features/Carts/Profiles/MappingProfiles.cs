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
            //CreateMap<Cart, GetListCartByUserViewModel>()
            //    .ForPath(dest => dest.Product.ProductName, opt => opt.MapFrom(src => src.CartItems.Select(x => x.Product.Name)))
            //    //.ForPath(dest => dest.Product.ProductId, opt => opt.MapFrom(src => Convert.ToInt16(src.CartItems.Select(x => x.Product.Id))))
            //    //.ForPath(dest => dest.Product.ProductPrice, opt => opt.MapFrom(src => src.CartItems.Select(x => x.Product.Price)))
            //    //.ForPath(dest => dest.Quantity, opt => opt.MapFrom(src => src.CartItems.Select(x => x.Quantity)))
            //    .ForPath(dest => dest.Product.Description, opt => opt.MapFrom(src => src.CartItems.Select(x => x.Product.Description)))
            //    .ForPath(dest => dest.Product.BodyImage, opt => opt.MapFrom(src => src.CartItems.Select(x => x.Product.BodyImage)))
            //.ForPath(dest => dest.Product.ProductStatus, opt => opt.MapFrom(src => src.CartItems.Select(x => x.Product.Status)))
            ////.ForPath(dest => dest.Product.Stock, opt => opt.MapFrom(src => src.CartItems.Select(x => x.Product.Stock)))
            ////.ForPath(dest => dest.Product.Rating, opt => opt.MapFrom(src => src.CartItems.Select(x => x.Product.Rating)))
            //.ReverseMap();
        }
    }
}
