using DailyShop.Business.Features.Categories.DailyFrontendDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyShop.Business.Features.Categories.Models
{
    public class GetListCategoryModel
    {
        public List<GetListCategoryFrontendDto> GetListCategoryFrontendDtos { get; set; }
    }
}
