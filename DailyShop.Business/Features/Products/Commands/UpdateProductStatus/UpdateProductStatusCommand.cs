using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.CrossCuttingConcerns.Exceptions;
using DailyShop.Business.Features.Products.Dtos;
using DailyShop.Business.Services.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DailyShop.Business.Features.Products.Commands.UpdateProductStatus
{
    public class UpdateProductStatusCommand:IRequest
    {
        public int ProductId { get; set; }
        public bool Status { get; set; }
        public class UpdateProductStatusCommandHandler:IRequestHandler<UpdateProductStatusCommand>
        {
            private readonly IProductRepository _productRepository;

            public UpdateProductStatusCommandHandler(IProductRepository productRepository)
            {
                _productRepository = productRepository;
            }
            public async Task Handle(UpdateProductStatusCommand request, CancellationToken cancellationToken)
            {
                var product = await _productRepository.Query().FirstOrDefaultAsync(p => p.Id == request.ProductId);
                if (product == null)
                    throw new BusinessException("Böyle bir ürün bulunamadı.");

                if (request.Status == null)
                    product.Status = "Onay bekliyor.";
                else if (request.Status == true)
                    product.Status = "Onaylandı.";
                else
                    product.Status = "Onaylanmadı.";
                await _productRepository.UpdateAsync(product);
            }
        }
    }
}
