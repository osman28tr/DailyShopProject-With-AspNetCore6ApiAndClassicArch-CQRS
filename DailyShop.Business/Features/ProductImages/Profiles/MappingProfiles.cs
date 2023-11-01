using AutoMapper;
using DailyShop.Business.Features.ProductImages.Dtos;
using DailyShop.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyShop.Business.Features.ProductImages.Profiles
{
    public class MappingProfiles:Profile
    {
        public MappingProfiles()
        {
            CreateMap<ProductImage, InsertedProductImageDto>().ReverseMap();
        }
    }
}
