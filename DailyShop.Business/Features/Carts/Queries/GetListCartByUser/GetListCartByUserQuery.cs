﻿using AutoMapper;
using Core.CrossCuttingConcerns.Exceptions;
using DailyShop.Business.Features.Carts.Models;
using DailyShop.Business.Features.Products.Models;
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
                    .Include(ci => ci.CartItems).ThenInclude(p => p.Product).ThenInclude(pi => pi.ProductImages)
                    .Include(ci => ci.CartItems).ThenInclude(p => p.Product).ThenInclude(product => product.Colors)
                    .ThenInclude(c => c.Color)
                    .Include(ci => ci.CartItems).ThenInclude(p => p.Product).ThenInclude(product => product.Sizes)
                    .ThenInclude(c => c.Size)
                    .Include(ci => ci.CartItems).ThenInclude(p => p.Product).ThenInclude(product => product.Reviews)
                    .Include(c => c.CartItems)
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
                            Quantity = cartItem.Quantity,
                            Id = cartItem.Id,
                        };
                        cartItemModel.Product.BodyImage = cartItem.Product.BodyImage;
                        foreach (var review in cartItem.Product.Reviews)
                        {
                            GetListReviewByProductViewModel cartItemReview = new()
                            {
                                Name = review.Name,
                                ReviewAvatar = review.Avatar,
                                ReviewRating = review.Rating,
                                ReviewDescription = review.Description,
                                ReviewStatus = review.Status,
                                ReviewCreatedDate = review.CreatedAt,
                                ReviewUpdatedDate = review.UpdatedAt
                            };
                            cartItemModel.Product.Reviews.Add(cartItemReview);
                        }
                        mappedCartItem.Add(cartItemModel);
                    }
                });
                return mappedCartItem;
            }
        }
    }
}
