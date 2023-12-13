using AutoMapper;
using DailyShop.Business.Features.Categories.Dtos;
using DailyShop.Business.Features.Products.Dtos;
using DailyShop.Business.Services.Repositories;
using DailyShop.Entities.Concrete;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.CrossCuttingConcerns.Exceptions;
using DailyShop.Business.Services.Repositories.Dapper;
using Microsoft.AspNetCore.Http.Extensions;


namespace DailyShop.Business.Features.Products.Queries.GetListProduct
{
    public class GetListProductQuery : IRequest<List<GetListProductDto>>
    {
        public class GetListProductQueryHandler : IRequestHandler<GetListProductQuery, List<GetListProductDto>>
        {
            private readonly IProductRepository _productRepository;
            private readonly IDpProductRepository _dpProductRepository;
            private readonly IMapper _mapper;

            public GetListProductQueryHandler(IProductRepository productRepository, IDpProductRepository dpProductRepository, IMapper mapper)
            {
                _productRepository = productRepository;
                _dpProductRepository = dpProductRepository;
                _mapper = mapper;
            }

            public async Task<List<GetListProductDto>> Handle(GetListProductQuery request, CancellationToken cancellationToken)
            {
                List<Product> products = await _productRepository.Query().Include(p => p.User).ToListAsync();

                if (products == null)
                    throw new BusinessException("Herhangi bir ürün bulunamadı.");

                List<GetListProductDto> mappedGetListProduct = _mapper.Map<List<GetListProductDto>>(products);

                foreach (var mappedProduct in mappedGetListProduct)
                {
                    var productColors = await _dpProductRepository.GetProductDetailColorByIdAsync(mappedProduct.Id);
                    if (productColors != null)
                    {
                        foreach (var productColor in productColors)
                        {
                            mappedProduct.Colors.Add(productColor);
                        }
                    }
                    var productSizes = await _dpProductRepository.GetProductDetailSizeByIdAsync(mappedProduct.Id);
                    if (productSizes != null)
                    {
                        foreach (var productSize in productSizes)
                        {
                            mappedProduct.Sizes.Add(productSize);
                        }
                    }
                    var productImages = await _dpProductRepository.GetProductDetailImageByIdAsync(mappedProduct.Id);
                    if (productImages != null)
                    {
                        foreach (var productImage in productImages)
                        {
                            mappedProduct.ProductImages.Add(productImage);
                        }
                    }
                }

                foreach (var mappedProduct in mappedGetListProduct)
                {
                    if (mappedProduct.SellerName == "admin admin")
                    {
                        mappedProduct.SellerName = "DailyShop";
                    }
                }

                return mappedGetListProduct;
            }
        }
    }
}
