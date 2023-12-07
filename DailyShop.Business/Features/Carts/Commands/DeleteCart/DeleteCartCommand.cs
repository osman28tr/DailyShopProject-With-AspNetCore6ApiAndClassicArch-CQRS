using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DailyShop.Business.Services.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DailyShop.Business.Features.Carts.Commands.DeleteCart
{
    public class DeleteCartCommand:IRequest
    {
        public int CartItemId { get; set; }
        public int UserId { get; set; }
        public class DeleteCartCommandHandler:IRequestHandler<DeleteCartCommand>
        {
            private readonly ICartRepository _cartRepository;

            public DeleteCartCommandHandler(ICartRepository cartRepository)
            {
                _cartRepository = cartRepository;
            }

            public async Task Handle(DeleteCartCommand request, CancellationToken cancellationToken)
            {
                var carts = await _cartRepository.Query().Where(x => x.UserId == request.UserId)
                    .Include(ci => ci.CartItems).ToListAsync();
                carts.ForEach(cart =>
                {
                    foreach (var cartCartItem in cart.CartItems)
                    {
                        if (cartCartItem.Id == request.CartItemId)
                        {
                            cart.CartItems.Remove(cartCartItem);
                            _cartRepository.UpdateAsync(cart);
                            return;
                        }
                    }
                });
            }
        }
    }
}
