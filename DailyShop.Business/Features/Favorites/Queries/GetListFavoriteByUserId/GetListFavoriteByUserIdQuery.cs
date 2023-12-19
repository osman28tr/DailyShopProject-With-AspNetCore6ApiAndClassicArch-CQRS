using AutoMapper;
using DailyShop.Business.Features.Favorites.Models;
using DailyShop.Business.Services.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyShop.Business.Features.Favorites.Queries.GetListFavoriteByUserId
{
    public class GetListFavoriteByUserIdQuery:IRequest<List<GetListFavoriteByUserIdViewModel>>
    {
        public int UserId { get; set; }
        public class GetListFavoriteByUserIdQueryHandler : IRequestHandler<GetListFavoriteByUserIdQuery, List<GetListFavoriteByUserIdViewModel>>
        {
            private readonly IFavoriteRepository _favoriteRepository;
            private readonly IMapper _mapper;
            public GetListFavoriteByUserIdQueryHandler(IFavoriteRepository favoriteRepository, IMapper mapper)
            {
                _favoriteRepository = favoriteRepository;
                _mapper = mapper;
            }
            public async Task<List<GetListFavoriteByUserIdViewModel>> Handle(GetListFavoriteByUserIdQuery request, CancellationToken cancellationToken)
            {
                var listFavorite = await _favoriteRepository.Query().Where(x => x.UserId == request.UserId).Include(x => x.Product).ToListAsync();
                var mappedListFavorite = _mapper.Map<List<GetListFavoriteByUserIdViewModel>>(listFavorite);
                return mappedListFavorite;
            }
        }
    }
}
