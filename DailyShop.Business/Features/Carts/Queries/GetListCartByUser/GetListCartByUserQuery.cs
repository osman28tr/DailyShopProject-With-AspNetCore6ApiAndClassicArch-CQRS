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
				var cart = await _cartRepository.Query().Where(c => c.UserId == request.UserId)
					.Include(c => c.CartItems).Include(u => u.User).FirstOrDefaultAsync(cancellationToken: cancellationToken);

				if (cart == null)
					throw new BusinessException("Sepetinizde hiçbir ürün yok.");

				foreach (var item in cart.CartItems)
				{
					var product = await _dpProductRepository.GetProductByIdAsync(item.ProductId);
					item.Product = product;
					item.ProductId = product.Id;
				}
				var mappedCartItem = new List<GetListCartByUserViewModel>();

				mappedCartItem.AddRange(cart.CartItems.Select(cartItem => new GetListCartByUserViewModel()
				{
					Quantity = cartItem.Quantity,
					Id = cartItem.Id,
					Color = cartItem.Color,
					Size = cartItem.Size,
					Product = { BodyImage = cartItem.Product?.BodyImage, Name = cartItem.Product?.Name, Price = cartItem.Product?.Price, Id = cartItem.ProductId }
				}));
				return mappedCartItem;

			}
		}
	}
}
