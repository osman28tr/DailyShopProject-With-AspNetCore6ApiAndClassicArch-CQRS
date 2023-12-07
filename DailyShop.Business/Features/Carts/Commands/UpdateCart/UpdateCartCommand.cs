using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DailyShop.Business.Features.Carts.Dtos;
using DailyShop.Business.Services.Repositories;
using DailyShop.Entities.Concrete;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DailyShop.Business.Features.Carts.Commands.UpdateCart
{
    public class UpdateCartCommand:IRequest
    {
        public int CartItemId { get; set; }
        public int UserId { get; set; }
        public UpdatedCartItemDto UpdatedCartItemDto { get; set; }
        public class UpdateCartCommandHandler:IRequestHandler<UpdateCartCommand>
        {
            private readonly ICartRepository _cartRepository;

            public UpdateCartCommandHandler(ICartRepository cartRepository)
            {
                _cartRepository = cartRepository;
            }
            public async Task Handle(UpdateCartCommand request, CancellationToken cancellationToken)
            {
                var cartList = await _cartRepository.Query().Where(x => x.UserId == request.UserId)
                    .Include(ci => ci.CartItems).ToListAsync();

                cartList.ForEach(cart =>
                {
                    foreach (var cartCartItem in cart.CartItems)
                    {
                        if (cartCartItem.Id == request.CartItemId)
                        {
                            cartCartItem.Quantity = request.UpdatedCartItemDto.Quantity;
                            _cartRepository.UpdateAsync(cart);
                            return;
                        }
                    }
                });
            }
        }
    }
}
