using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DailyShop.Business.Features.Carts.Dtos;
using DailyShop.Business.Services.Repositories;
using DailyShop.Entities.Concrete;
using MediatR;

namespace DailyShop.Business.Features.Carts.Commands.InsertCart
{
    public class InsertCartCommand:IRequest
    {
        public List<InsertedCartItemDto> InsertedCartItemDtos { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public class InsertCartCommandHandler:IRequestHandler<InsertCartCommand>
        {
            private readonly IMapper _mapper;
            private readonly ICartRepository _cartRepository;

            public InsertCartCommandHandler(IMapper mapper, ICartRepository cartRepository)
            {
                _mapper = mapper;
                _cartRepository = cartRepository;
            }

            public async Task Handle(InsertCartCommand request, CancellationToken cancellationToken)
            {
                Cart cart = new() { UserId = request.UserId, Status = "" };
                if (request.InsertedCartItemDtos.Any())
                {
                    foreach (var cartItem in request.InsertedCartItemDtos)
                    {
                        cart.CartItems.Add(new CartItem()
                        {
                            ProductId = request.ProductId,
                            Quantity = cartItem.Quantity,
                            TotalPrice = cartItem.TotalPrice
                        });
                    }
                }

                await _cartRepository.AddAsync(cart);
            }
        }
    }
}
