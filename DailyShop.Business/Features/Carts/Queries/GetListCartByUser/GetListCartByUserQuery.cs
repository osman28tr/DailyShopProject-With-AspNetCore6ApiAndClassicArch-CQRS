using AutoMapper;
using Core.CrossCuttingConcerns.Exceptions;
using DailyShop.Business.Features.Carts.Models;
using DailyShop.Business.Services.Repositories;
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

            public GetListCartByUserQueryHandler(IMapper mapper, ICartRepository cartRepository)
            {
                _mapper = mapper;
                _cartRepository = cartRepository;
            }

            public async Task<List<GetListCartByUserViewModel>> Handle(GetListCartByUserQuery request,
                CancellationToken cancellationToken)
            {
                var cartItems = await _cartRepository.Query().Where(c => c.UserId == request.UserId)
                    .Include(ci => ci.CartItems).ThenInclude(p => p.Product).ThenInclude(pi=>pi.ProductImages)
                    .Include(ci => ci.CartItems).ThenInclude(p => p.Product).ThenInclude(product => product.Colors).ThenInclude(c=>c.Color)
                    .Include(ci => ci.CartItems).ThenInclude(p => p.Product).ThenInclude(product => product.Sizes).ThenInclude(c => c.Size)
                    .ToListAsync(cancellationToken: cancellationToken);

                if (cartItems == null)
                     throw new BusinessException("Sepetinizde hiçbir ürün yok.");

                var mappedCartItem = new List<GetListCartByUserViewModel>();

                cartItems.ForEach(c =>
                {
                    foreach (var cartItem in c.CartItems)
                    {
                        GetListCartByUserViewModel cartItemModel = new()
                        {
                            Status = c.Status,
                            ProductName = cartItem.Product.Name,
                            ProductId = cartItem.ProductId,
                            ProductPrice = cartItem.Product.Price,
                            Quantity = cartItem.Quantity,
                            TotalPrice = cartItem.TotalPrice,
                            CartItemId = cartItem.Id,
                            BodyImage = cartItem.Product.BodyImage,
                            Description = cartItem.Product.Description,
                            ProductStatus = cartItem.Product.Status,
                            Stock = cartItem.Product.Stock,
                            Rating = cartItem.Product.Rating,
                            ProductImages = cartItem.Product.ProductImages.Select(pi=>pi.Name).ToList(),
                        };
                        cartItems.ForEach(c =>
                        {
                            foreach (var cartItem in c.CartItems)
                            {
                                foreach (var color in cartItem.Product.Colors)
                                {
                                    cartItemModel.Colors.Add(color.Color.Name);
                                }

                                foreach (var size in cartItem.Product.Sizes)
                                {
                                    cartItemModel.Sizes.Add(size.Size.Name);
                                }
                            }
                        });
                        mappedCartItem.Add(cartItemModel);
                    }
                });
                return mappedCartItem;
            }
        }
    }
}
