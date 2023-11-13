using AutoMapper;
using DailyShop.Business.Features.Products.Dtos;
using DailyShop.Business.Features.Products.Models;
using DailyShop.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyShop.Business.Features.Products.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product, GetListProductDto>()
                .ForMember(dest => dest.SellerName, opt => opt.MapFrom(src => src.User.FirstName + " " + src.User.LastName))
                .ForMember(dest => dest.Colors, opt => opt.MapFrom(src => src.Colors.Select(x => x.Color.Name)))
                .ForMember(dest => dest.Sizes, opt => opt.MapFrom(src => src.Sizes.Select(x => x.Name)))
                .ForMember(dest => dest.ProductImages, opt => opt.MapFrom(src => src.ProductImages.Select(x => x.Name)))
                .ReverseMap();

            CreateMap<Product, GetListProductByCategoryAndIsDeleteViewModel>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.FirstName + " " + src.User.LastName))
                .ForMember(dest => dest.Colors, opt => opt.MapFrom(src => src.Colors.Select(x => x.Color.Name)))
                .ForMember(dest => dest.Sizes, opt => opt.MapFrom(src => src.Sizes.Select(x => x.Name)))
                .ForMember(dest => dest.ProductImages, opt => opt.MapFrom(src => src.ProductImages.Select(x => x.Name)))
                .ReverseMap();

            CreateMap<Product, GetByIdProductDetailViewModel>()
	            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.FirstName + " " + src.User.LastName))
	            .ForMember(dest => dest.Colors, opt => opt.MapFrom(src => src.Colors.Select(x => x.Color.Name)))
	            .ForMember(dest => dest.Sizes, opt => opt.MapFrom(src => src.Sizes.Select(x => x.Name)))
	            .ForMember(dest => dest.ProductImages, opt => opt.MapFrom(src => src.ProductImages.Select(x => x.Name)))
	            .ReverseMap();

			CreateMap<Product, InsertedProductDto>()
                .ReverseMap();

            CreateMap<Product, InsertProductViewModel>().ReverseMap();
            CreateMap<ProductImage, InsertedProductImageDto>().ReverseMap();
            CreateMap<Color, InsertedProductColorDto>().ReverseMap();
            CreateMap<Size, InsertedProductSizeDto>().ReverseMap();
        }
        private List<string> GetReviewNames(ICollection<Review> reviews)
        {
            return reviews.Select(x => x.Name).ToList();
        }

        private List<string> GetReviewDescriptions(ICollection<Review> reviews)
        {
            return reviews.Select(x => x.Description).ToList();
        }
    }
}
