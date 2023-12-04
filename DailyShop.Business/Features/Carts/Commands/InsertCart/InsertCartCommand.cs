using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Core.CrossCuttingConcerns.Exceptions;
using DailyShop.Business.Features.Carts.Dtos;
using DailyShop.Business.Services.Repositories;
using DailyShop.Entities.Concrete;
using MediatR;

namespace DailyShop.Business.Features.Carts.Commands.InsertCart
{
    public class InsertCartCommand:IRequest
    {
        public InsertedCartItemDto InsertedCartItemDto { get; set; }
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
                try
                {
                    Cart cartByUser = _cartRepository.Query().FirstOrDefault(c => c.UserId == request.UserId);
                    if (cartByUser == null)
                    {
                        Cart cart = new() { UserId = request.UserId, Status = "" };
                        cart.CartItems.Add(new CartItem()
                        {
                            ProductId = request.ProductId,
                            Quantity = request.InsertedCartItemDto.Quantity,
                            TotalPrice = request.InsertedCartItemDto.TotalPrice,
                            Color = request.InsertedCartItemDto.Color,
                            Size = request.InsertedCartItemDto.Size,
                        });
                        await _cartRepository.AddAsync(cart);
                    }
                    else
                    {
                        cartByUser.CartItems.Add(new CartItem()
                        {
                            ProductId = request.ProductId,
                            Quantity = request.InsertedCartItemDto.Quantity,
                            TotalPrice = request.InsertedCartItemDto.TotalPrice,
                            Color = request.InsertedCartItemDto.Color,
                            Size = request.InsertedCartItemDto.Size,
                        });
                        await _cartRepository.UpdateAsync(cartByUser);
                    }
                }
                catch (Exception hata)
                {
                    throw new BusinessException("Sepete eklerken bir hata oluştu.");
                }
            }
        }
    }
}
