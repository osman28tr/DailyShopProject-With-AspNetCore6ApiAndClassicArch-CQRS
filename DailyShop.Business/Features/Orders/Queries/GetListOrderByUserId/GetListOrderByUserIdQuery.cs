using AutoMapper;
using Core.CrossCuttingConcerns.Exceptions;
using DailyShop.Business.Features.Orders.Models;
using DailyShop.Business.Services.Repositories;
using DailyShop.Entities.Concrete;
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
                var orders = await _orderRepository.Query().Where(x => x.UserId == request.UserId).Include(oi => oi.OrderItems)!.ThenInclude(p => p.Product).ThenInclude(p=>p.Reviews).Include(x => x.Payment).Include(x => x.OrderAddress).ToListAsync(cancellationToken: cancellationToken);
				orders.ForEach(o => o.OrderItems.ToList().ForEach(oi => oi.Product.Rating = oi.Product.Reviews is { Count: > 0 } ? (byte)oi.Product.Reviews.Average(x => x.Rating)! : (byte)0));

				if (orders == null)
					throw new BusinessException("Herhangi bir siparişiniz yoktur.");
                var mappedOrder = _mapper.Map<List<GetListOrderByUserIdViewModel>>(orders);
                return mappedOrder;
            }
        }
    }
}
