using AutoMapper;
using DailyShop.Business.Features.Categories.Dtos;
using DailyShop.Business.Features.Categories.Models;
using DailyShop.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyShop.Business.Features.Categories.Profiles
{
    public class MappingProfiles:Profile
    {
        public MappingProfiles()
        {
            CreateMap<Category,GetListCategoryDto>().ReverseMap();
            CreateMap<Category,InsertedCategoryDto>().ReverseMap();
            CreateMap<Category,InsertCategoryViewModel>().ReverseMap();
            CreateMap<Category,UpdatedCategoryDto>().ReverseMap();
            CreateMap<Category,UpdateCategoryViewModel>().ReverseMap();
            CreateMap<Category, DeleteCategoryViewModel>().ReverseMap();
        }
    }
}
