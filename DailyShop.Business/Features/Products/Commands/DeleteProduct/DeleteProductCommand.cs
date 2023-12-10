using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Core.CrossCuttingConcerns.Exceptions;
using DailyShop.Business.Features.Products.Models;
using DailyShop.Business.Services.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DailyShop.Business.Features.Products.Commands.DeleteProduct
{
    public class DeleteProductCommand : IRequest<DeleteProductViewModel>
    {
        public int ProductId { get; set; }
        public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand,DeleteProductViewModel>
        {
            private readonly IProductRepository _productRepository;
            private readonly IMapper _mapper;

            public DeleteProductCommandHandler(IProductRepository productRepository,IMapper mapper)
            {
                _productRepository = productRepository;
                _mapper = mapper;
            }

            public async Task<DeleteProductViewModel> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
            {
                var deletedProduct = await _productRepository.Query().Where(p => p.Id == request.ProductId)
                    .Include(pi => pi.ProductImages).FirstOrDefaultAsync();
                if (deletedProduct != null)
                {
                    deletedProduct.IsDeleted = true;
                    deletedProduct.UpdatedAt = DateTime.Now;
                    await _productRepository.UpdateAsync(deletedProduct);
                    var mappedDeletedProduct = _mapper.Map<DeleteProductViewModel>(deletedProduct);
                    return mappedDeletedProduct;
                }
                throw new BusinessException("Ürün bulunamadı.");
            }
        }
    }
}
