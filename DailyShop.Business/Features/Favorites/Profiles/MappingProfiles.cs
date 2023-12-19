using AutoMapper;
using DailyShop.Business.Features.Favorites.Models;
using DailyShop.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyShop.Business.Features.Favorites.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Favorite, GetListFavoriteByUserIdViewModel>()
                .ForMember(x => x.Product, opt => opt.MapFrom(y => y.Product))
                .ReverseMap();
            CreateMap<Product, GetListProductAtFavorite>()
                .ReverseMap();
        }
    }
}
