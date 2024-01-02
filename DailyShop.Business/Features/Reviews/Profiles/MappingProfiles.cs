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
using DailyShop.Business.Features.Reviews.Models;
using DailyShop.Business.Features.Addresses.Dtos;
using DailyShop.Business.Features.AppUsers.Dtos;

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

            CreateMap<ReportReview, GetListReviewByReportViewModel>()
                .ForMember(x => x.ReporterUser, opt => opt.MapFrom(x => x.ReporterUser))
                .ForMember(x => x.Review, opt => opt.Ignore())
                //.ForPath(x => x.Review.User, opt => opt.MapFrom(x => x.Review.AppUser))
                //.ForPath(x => x.Review.User.Reviews, opt => opt.Ignore())
                //.ForPath(x => x.Review.User.Addresses, opt => opt.Ignore())
                .ForPath(x => x.ReporterUser.Reviews, opt => opt.Ignore())
                .ForPath(x => x.ReporterUser.Addresses, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<AppUser, GetListUserDto>()
                 .ForMember(x => x.Reviews, opt => opt.Ignore())
                .ReverseMap();
            //CreateMap<ReportUser, GetListUserByReportDto>()
            //    .ForMember(x => x.User, opt => opt.MapFrom(x => x.User))
            //    .ForPath(x => x.ReporterUser.Addresses, opt => opt.MapFrom(x => x.User.Addresses))
            //    .ForMember(x => x.ReporterUser, opt => opt.MapFrom(x => x.ReporterUser))
            //    .ForPath(x => x.User.Reviews, opt => opt.Ignore())
            //    .ForPath(x => x.ReporterUser.Reviews, opt => opt.Ignore())
            //    .ReverseMap();
            //CreateMap<Address, AddressListByUserIdDto>()
            //    .ReverseMap();
            CreateMap<Review, InsertedReviewDto>().ReverseMap();
            CreateMap<Review, InsertedReviewToReviewDto>().ReverseMap();
        }
    }
}
