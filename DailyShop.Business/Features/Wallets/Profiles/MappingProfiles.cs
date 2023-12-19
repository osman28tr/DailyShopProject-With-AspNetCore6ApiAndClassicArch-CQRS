using AutoMapper;
using DailyShop.Business.Features.Wallets.Models;
using DailyShop.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyShop.Business.Features.Wallets.Profiles
{
    public class MappingProfiles:Profile
    {
        public MappingProfiles()
        {
            CreateMap<Wallet, GetWalletViewModel>()
                .ReverseMap();
        }
    }
}
