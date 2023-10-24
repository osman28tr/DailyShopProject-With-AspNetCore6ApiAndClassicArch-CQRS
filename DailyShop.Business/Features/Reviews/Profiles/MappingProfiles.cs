using AutoMapper;
using DailyShop.Business.Features.Reviews.Dtos;
using DailyShop.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyShop.Business.Features.Reviews.Profiles
{
    public class MappingProfiles:Profile
    {
        public MappingProfiles()
        {
            CreateMap<Review, GetListReviewByUserIdDto>()
                .ForMember(r => r.Date, opt => opt.MapFrom(c => c.CreatedAt))
                .ReverseMap();
        }
    }
}
