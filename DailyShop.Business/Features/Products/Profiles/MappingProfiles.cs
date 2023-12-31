﻿using AutoMapper;
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
                .ReverseMap();

            CreateMap<Product, GetListProductByCategoryAndIsDeleteViewModel>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.FirstName + " " + src.User.LastName))
                .ForMember(dest => dest.Colors, opt => opt.MapFrom(src => src.Colors.Select(x => x.Color.Name)))
                .ForMember(dest => dest.Sizes, opt => opt.MapFrom(src => src.Sizes.Select(x => x.Size.Name)))
                .ForMember(dest => dest.ProductImages, opt => opt.MapFrom(src => src.ProductImages.Select(x => x.Name)))
                .ReverseMap();

            CreateMap<Product, GetProductDetailByIdViewModel>()
                //.ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.FirstName + " " + src.User.LastName))
                //.ForMember(dest => dest.Colors, opt => opt.MapFrom(src => src.Colors.Select(x => x.Color.Name)))
                //.ForMember(dest => dest.Sizes, opt => opt.MapFrom(src => src.Sizes.Select(x => x.Size.Name)))
                //.ForMember(dest => dest.ProductImages, opt => opt.MapFrom(src => src.ProductImages.Select(x => x.Name)))
                .ForMember(x => x.Category, opt => opt.MapFrom(x => x.Category))
                .ReverseMap();

            CreateMap<Category, GetCategoryAtGetProductDetail>().ReverseMap();

            CreateMap<Product, GetListProductByUserIdViewModel>()
                .ForMember(dest => dest.Description, opt => opt.Ignore())
                .ForMember(dest => dest.Colors, opt => opt.MapFrom(src => src.Colors.Select(x => x.Color.Name)))
                .ForMember(dest => dest.Sizes, opt => opt.MapFrom(src => src.Sizes.Select(x => x.Size.Name)))
                .ForMember(dest => dest.ProductImages, opt => opt.MapFrom(src => src.ProductImages.Select(x => x.Name)))

                .ReverseMap();

            CreateMap<Product, GetProductImagesByIdViewModel>()
                .ForMember(dest => dest.BodyImage, opt => opt.MapFrom(src => src.BodyImage))
                .ForMember(dest => dest.ProductImages, opt => opt.MapFrom(src => src.ProductImages.Select(x => x.Name)))
                .ReverseMap();

            CreateMap<Product, InsertedProductDto>()
                .ReverseMap();


            CreateMap<Product, InsertProductViewModel>().ReverseMap();
            CreateMap<ProductImage, InsertedProductImageDto>().ReverseMap();
            CreateMap<Color, InsertedProductColorDto>().ReverseMap();
            CreateMap<Size, InsertedProductSizeDto>().ReverseMap();

            CreateMap<Product, UpdatedProductDto>()
                .ForMember(x => x.ProductImages, opt => opt.Ignore())
                .ForMember(x => x.ProductImagesFile, opt => opt.Ignore())
                .ForMember(x => x.BodyImage, opt => opt.Ignore())
                .ForMember(x => x.Colors, opt => opt.Ignore())
                .ForMember(x => x.Sizes, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<Product, DeleteProductViewModel>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.BodyImage, opt => opt.MapFrom(src => src.BodyImage))
                .ForMember(dest => dest.ProductImages, opt => opt.MapFrom(src => src.ProductImages.Select(x => x.Name)))
                .ReverseMap();
        }
    }
}
