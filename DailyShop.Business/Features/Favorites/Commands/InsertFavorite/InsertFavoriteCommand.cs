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
    public class InsertFavoriteCommand : IRequest
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
                    var isFavorite = await _favoriteRepository.GetAsync(x => x.UserId == request.UserId && x.ProductId == request.ProductId);
                    if (isFavorite != null)
                        throw new BusinessException("Seçtiğiniz ürün favorilerinizde mevcut.");
                    var newFavorite = new Favorite() { ProductId = request.ProductId, UserId = request.UserId };
                    await _favoriteRepository.AddAsync(newFavorite);
                }
                catch (Exception hata)
                {
                    throw new BusinessException(hata.Message);
                }
            }
        }
    }
}
