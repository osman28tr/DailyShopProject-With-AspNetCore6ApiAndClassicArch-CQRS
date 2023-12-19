using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyShop.Business.Features.Favorites.Models
{
    public class GetListFavoriteByUserIdViewModel
    {
        public int? Id { get; set; }
        public GetListProductAtFavorite? Product { get; set; }
    }
}
