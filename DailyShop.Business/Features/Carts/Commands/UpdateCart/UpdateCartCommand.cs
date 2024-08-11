using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.CrossCuttingConcerns.Exceptions;
using DailyShop.Business.Features.Carts.Dtos;
using DailyShop.Business.Services.Repositories;
using DailyShop.Entities.Concrete;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DailyShop.Business.Features.Carts.Commands.UpdateCart
{
	public class UpdateCartCommand : IRequest
	{
		public int CartItemId { get; set; }
		public int UserId { get; set; }
		public UpdatedCartItemDto UpdatedCartItemDto { get; set; }
		public class UpdateCartCommandHandler : IRequestHandler<UpdateCartCommand>
		{
			private readonly ICartRepository _cartRepository;

			public UpdateCartCommandHandler(ICartRepository cartRepository)
			{
				_cartRepository = cartRepository;
			}
			public async Task Handle(UpdateCartCommand request, CancellationToken cancellationToken)
			{
				var cart = await _cartRepository.Query().Where(x => x.UserId == request.UserId)
					.Include(ci => ci.CartItems).FirstOrDefaultAsync(cancellationToken: cancellationToken) ?? throw new BusinessException("Ürün sepetinizde bulunamadı.");

				var cartItem = cart.CartItems.FirstOrDefault(x => x.Id == request.CartItemId) ??
							   throw new BusinessException("Ürün sepetinizde bulunamadı.");

				cartItem.Quantity = request.UpdatedCartItemDto.Quantity;
				await _cartRepository.UpdateAsync(cart);
			}
		}
	}
}
