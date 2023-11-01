using AutoMapper;
using DailyShop.Business.Features.ProductColors.Dtos;
using DailyShop.Business.Services.Repositories;
using DailyShop.Entities.Concrete;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyShop.Business.Features.ProductColors.Commands.InsertProductColor
{
    public class InsertProductColorCommand:IRequest
    {
        public ICollection<InsertedProductColorDto> InsertedProductColorDto { get; set; }
        public int ProductId { get; set; }
        public class InsertProductColorCommandHandler : IRequestHandler<InsertProductColorCommand>
        {
            private readonly IProductColorRepository _repository;
            private readonly IMapper _mapper;
            public async Task Handle(InsertProductColorCommand request, CancellationToken cancellationToken)
            {
                ICollection<Color> productColors = _mapper.Map<ICollection<Color>>(request.InsertedProductColorDto);
                foreach (var productColor in productColors)
                {                   
                    await _repository.AddAsync(productColor);
                }
            }
        }
    }
}
