using AutoMapper;
using Core.CrossCuttingConcerns.Exceptions;
using DailyShop.Business.Features.Orders.Models;
using DailyShop.Business.Services.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyShop.Business.Features.Orders.Queries.GetListOrderByUserId
{
    public class GetListOrderByUserIdQuery:IRequest<List<GetListOrderByUserIdViewModel>>
    {
        public int UserId { get; set; }
        public class GetListOrderByUserIdQueryHandler : IRequestHandler<GetListOrderByUserIdQuery, List<GetListOrderByUserIdViewModel>>
        {
            private readonly IOrderRepository _orderRepository;
            private readonly IMapper _mapper;
            public GetListOrderByUserIdQueryHandler(IOrderRepository orderRepository, IMapper mapper)
            {
                _orderRepository = orderRepository;
                _mapper = mapper;
            }
            public async Task<List<GetListOrderByUserIdViewModel>> Handle(GetListOrderByUserIdQuery request, CancellationToken cancellationToken)
            {
                var order = await _orderRepository.Query().Where(x => x.UserId == request.UserId).Include(oi => oi.OrderItems).ThenInclude(p => p.Product).Include(x => x.Payment).Include(x => x.OrderAddress).ToListAsync();
                if (order == null)
                    throw new BusinessException("Herhangi bir siparişiniz yoktur.");
                var mappedOrder = _mapper.Map<List<GetListOrderByUserIdViewModel>>(order);
                return mappedOrder;
            }
        }
    }
}
