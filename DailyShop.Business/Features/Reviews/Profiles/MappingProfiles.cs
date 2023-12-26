using AutoMapper;
using DailyShop.Business.Features.Reviews.Dtos;
using DailyShop.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DailyShop.Business.Features.Products.Models;
using Core.Security.Entities;

namespace DailyShop.Business.Features.Reviews.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Review, GetListReviewByUserIdDto>()
                .ForMember(r => r.CreatedAt, opt => opt.MapFrom(c => c.CreatedAt))
                .ForPath(x => x.Product.Name, opt => opt.MapFrom(src => src.Product.Name))
                .ForPath(x => x.Product.Id, opt => opt.MapFrom(src => src.Product.Id))
                .ForPath(x => x.Product.BodyImage, opt => opt.MapFrom(src => src.Product.BodyImage))
                //.AfterMap((src, dest, context) => dest.Product = context.Mapper.Map<GetListReviewByProductViewModel, Product>(src))
                .ReverseMap();
            CreateMap<Review, InsertedReviewDto>().ReverseMap();
            CreateMap<Review, InsertedReviewToReviewDto>().ReverseMap();
        }
    }
}
