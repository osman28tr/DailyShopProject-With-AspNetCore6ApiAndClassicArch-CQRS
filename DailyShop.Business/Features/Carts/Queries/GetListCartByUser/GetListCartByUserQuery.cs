using AutoMapper;
using Core.CrossCuttingConcerns.Exceptions;
using DailyShop.Business.Features.Carts.Models;
using DailyShop.Business.Features.Products.Models;
using DailyShop.Business.Services.Repositories;
using DailyShop.Business.Services.Repositories.Dapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DailyShop.Business.Features.Carts.Queries.GetListCartByUser
{
    public class GetListCartByUserQuery : IRequest<List<GetListCartByUserViewModel>>
    {
        public int UserId { get; set; }

        public class
            GetListCartByUserQueryHandler : IRequestHandler<GetListCartByUserQuery, List<GetListCartByUserViewModel>>
        {
            private readonly IMapper _mapper;
            private readonly ICartRepository _cartRepository;
            private readonly IDpProductRepository _dpProductRepository;

            public GetListCartByUserQueryHandler(IMapper mapper, ICartRepository cartRepository, IDpProductRepository dpProductRepository)
            {
                _mapper = mapper;
                _cartRepository = cartRepository;
                _dpProductRepository = dpProductRepository;
            }

            public async Task<List<GetListCartByUserViewModel>> Handle(GetListCartByUserQuery request,
                CancellationToken cancellationToken)
            {
                var cartItems = await _cartRepository.Query().Where(c => c.UserId == request.UserId)
                    .Include(c => c.CartItems)
                    .ToListAsync(cancellationToken: cancellationToken);

                if (cartItems == null)
                    throw new BusinessException("Sepetinizde hiçbir ürün yok.");

                foreach (var cartItem in cartItems)
                {
                    foreach (var item in cartItem.CartItems)
                    {
                        var product = await _dpProductRepository.GetProductByIdAsync(item.ProductId);
                        item.Product = product;
                        item.ProductId = product.Id;
                    }
                }
                var mappedCartItem = new List<GetListCartByUserViewModel>();

                cartItems.ForEach(c =>
                {
                    foreach (var cartItem in c.CartItems)
                    {
                        GetListCartByUserViewModel cartItemModel = new()
                        {
                            Quantity = cartItem.Quantity,
                            Id = cartItem.Id,
                            Color = cartItem.Color,
                            Size = cartItem.Size
                        };
                        cartItemModel.Product.BodyImage = cartItem.Product.BodyImage;
                        cartItemModel.Product.Name = cartItem.Product.Name;
                        cartItemModel.Product.Price = cartItem.Product.Price;
                        mappedCartItem.Add(cartItemModel);
                    }
                });
                return mappedCartItem;
            }
        }
    }
}
