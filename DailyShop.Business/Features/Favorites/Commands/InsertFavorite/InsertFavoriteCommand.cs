using AutoMapper;
using Core.CrossCuttingConcerns.Exceptions;
using DailyShop.Business.Services.Repositories;
using DailyShop.Entities.Concrete;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyShop.Business.Features.Favorites.Commands.InsertFavorite
{
    public class InsertFavoriteCommand:IRequest
    {
        public int ProductId { get; set; }
        public int UserId { get; set; }
        public class InsertFavoriteCommandHandler : IRequestHandler<InsertFavoriteCommand>
        {
            private readonly IFavoriteRepository _favoriteRepository;
            public InsertFavoriteCommandHandler(IFavoriteRepository favoriteRepository)
            {
                _favoriteRepository = favoriteRepository;
            }
            public async Task Handle(InsertFavoriteCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    var favorite = new Favorite() { ProductId = request.ProductId, UserId = request.UserId };
                    await _favoriteRepository.AddAsync(favorite);
                }
                catch (Exception hata)
                {
                    throw new BusinessException(hata.Message);
                }
            }
        }
    }
}
