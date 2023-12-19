using Core.CrossCuttingConcerns.Exceptions;
using DailyShop.Business.Services.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyShop.Business.Features.Favorites.Commands.DeleteFavorite
{
    public class DeleteFavoriteCommand : IRequest
    {
        public int ProductId { get; set; }
        public int UserId { get; set; }
        public class DeleteFavoriteCommandHandler : IRequestHandler<DeleteFavoriteCommand>
        {
            private readonly IFavoriteRepository _favoriteRepository;
            public DeleteFavoriteCommandHandler(IFavoriteRepository favoriteRepository)
            {
                _favoriteRepository = favoriteRepository;
            }
            public async Task Handle(DeleteFavoriteCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    var favorite = await _favoriteRepository.GetAsync(x => x.ProductId == request.ProductId && x.UserId == request.UserId);
                    if (favorite == null)
                        throw new BusinessException("Favori bilgisi bulunamadı.");
                    await _favoriteRepository.DeleteAsync(favorite, false);
                }
                catch (Exception hata)
                {
                    throw new BusinessException(hata.Message);
                }
            }
        }
    }
}
