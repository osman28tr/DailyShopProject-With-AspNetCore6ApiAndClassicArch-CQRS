using AutoMapper;
using DailyShop.Business.Features.ProductSizes.Dtos;
using DailyShop.Business.Services.Repositories;
using DailyShop.Entities.Concrete;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyShop.Business.Features.ProductSizes.Commands.InsertProductSize
{
    public class InsertProductSizeCommand:IRequest
    {
        public ICollection<InsertedProductSizeDto> InsertedProductSizeDto { get; set; }
        public class InsertProductSizeCommandHandler : IRequestHandler<InsertProductSizeCommand>
        {
            private readonly IProductSizeRepository _repository;
            private readonly IMapper _mapper;

            public InsertProductSizeCommandHandler(IProductSizeRepository repository, IMapper mapper)
            {
                _repository = repository;
                _mapper = mapper;
            }

            public async Task Handle(InsertProductSizeCommand request, CancellationToken cancellationToken)
            {
                ICollection<Size> productSizes = _mapper.Map<ICollection<Size>>(request.InsertedProductSizeDto);
                foreach (var productSize in productSizes)
                {
                    await _repository.AddAsync(productSize);
                }
            }
        }
    }
}
