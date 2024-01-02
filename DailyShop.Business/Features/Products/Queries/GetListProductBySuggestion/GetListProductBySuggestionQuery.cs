using AutoMapper;
using Core.CrossCuttingConcerns.Exceptions;
using DailyShop.Business.Features.Products.Dtos;
using DailyShop.Business.Services.Repositories;
using DailyShop.Business.Services.Repositories.Dapper;
using DailyShop.Entities.Concrete;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Org.BouncyCastle.Asn1.Cmp.Challenge;

namespace DailyShop.Business.Features.Products.Queries.GetListProductBySuggestion
{
    public class GetListProductBySuggestionQuery : IRequest<List<GetListProductDto>>
    {
        public int UserId { get; set; }
        public class GetListProductBySuggestionQueryHandler : IRequestHandler<GetListProductBySuggestionQuery, List<GetListProductDto>>
        {
            private readonly IOrderRepository _orderRepository;
            private readonly IProductRepository _productRepository;
            private readonly IDpProductRepository _dpProductRepository;
            private readonly IMapper _mapper;
            public GetListProductBySuggestionQueryHandler(IOrderRepository orderRepository, IMapper mapper, IDpProductRepository dpProductRepository, IProductRepository productRepository)
            {
                _orderRepository = orderRepository;
                _mapper = mapper;
                _dpProductRepository = dpProductRepository;
                _productRepository = productRepository;
            }
            public async Task<List<GetListProductDto>> Handle(GetListProductBySuggestionQuery request, CancellationToken cancellationToken)
            {

                var suggestionProductsByOrder = await _orderRepository.Query().Where(x => x.UserId == request.UserId).Include(x => x.OrderItems).ThenInclude(x => x.Product).SelectMany(x => x.OrderItems.Select(oi => oi.Product)).ToListAsync();

                var suggestionProducts = await _productRepository.Query().ToListAsync();
                var productListDto = new List<GetListProductDto>();
                var random = new Random();

                if (suggestionProductsByOrder == null)
                {
                    suggestionProductsByOrder = await _orderRepository.Query().Include(x => x.OrderItems).ThenInclude(x => x.Product).SelectMany(x => x.OrderItems.Select(oi => oi.Product)).ToListAsync();
                }
                if (suggestionProductsByOrder == null)
                {
                    var allIdsByProduct = await _productRepository.Query().Select(x => x.Id).ToListAsync();

                    var randomIdsByProduct = allIdsByProduct.OrderBy(x => random.Next()).Take(4).ToList();

                    suggestionProducts = suggestionProducts.Where(x => randomIdsByProduct.Contains(x.Id)).ToList();

                    foreach (var suggestionProduct in suggestionProducts)
                    {
                        var productDto = _mapper.Map<GetListProductDto>(suggestionProduct);
                        var productColors = await _dpProductRepository.GetProductDetailColorByIdAsync(productDto.Id);
                        if (productColors != null)
                        {
                            foreach (var productColor in productColors)
                            {
                                productDto.Colors.Add(productColor);
                            }
                        }
                        var productSizes = await _dpProductRepository.GetProductDetailSizeByIdAsync(productDto.Id);
                        if (productSizes != null)
                        {
                            foreach (var productSize in productSizes)
                            {
                                productDto.Sizes.Add(productSize);
                            }
                        }
                        var productImages = await _dpProductRepository.GetProductDetailImageByIdAsync(productDto.Id);
                        if (productImages != null)
                        {
                            foreach (var productImage in productImages)
                            {
                                productDto.ProductImages.Add(productImage);
                            }
                        }
                        productListDto.Add(productDto);
                    }
                    return productListDto;
                }

                var allIdsByOrder = await _orderRepository.Query().Where(x => x.UserId == request.UserId).Include(x => x.OrderItems).ThenInclude(x => x.Product).SelectMany(x => x.OrderItems.Select(oi => oi.Product.Id)).ToListAsync();

                var randomIdsByOrder = allIdsByOrder.OrderBy(x => random.Next()).Take(4).ToList();

                suggestionProductsByOrder = suggestionProductsByOrder.Where(x => randomIdsByOrder.Contains(x.Id)).ToList();

                foreach (var product in suggestionProductsByOrder)
                {
                    var productDto = _mapper.Map<GetListProductDto>(product);
                    var productColors = await _dpProductRepository.GetProductDetailColorByIdAsync(productDto.Id);
                    if (productColors != null)
                    {
                        foreach (var productColor in productColors)
                        {
                            productDto.Colors.Add(productColor);
                        }
                    }
                    var productSizes = await _dpProductRepository.GetProductDetailSizeByIdAsync(productDto.Id);
                    if (productSizes != null)
                    {
                        foreach (var productSize in productSizes)
                        {
                            productDto.Sizes.Add(productSize);
                        }
                    }
                    var productImages = await _dpProductRepository.GetProductDetailImageByIdAsync(productDto.Id);
                    if (productImages != null)
                    {
                        foreach (var productImage in productImages)
                        {
                            productDto.ProductImages.Add(productImage);
                        }
                    }
                    productListDto.Add(productDto);
                }
                return productListDto;
            }
        }
    }
}
