using Core.CrossCuttingConcerns.Exceptions;
using DailyShop.Business.Services.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyShop.Business.Features.Orders.Queries.GetOrderById
{
    public class GetOrderByIdCommand:IRequest
    {
        public int? UserId { get; set; }
        public int? OrderId { get; set; }
        public class GetOrderByIdCommandHandler : IRequestHandler<GetOrderByIdCommand>
        {
            private readonly IOrderRepository _orderRepository;
            private readonly IAppUserRepository _userRepository;
            public GetOrderByIdCommandHandler(IOrderRepository orderRepository, IAppUserRepository userRepository)
            {
                _orderRepository = orderRepository;
                _userRepository = userRepository;
            }
            public async Task Handle(GetOrderByIdCommand request, CancellationToken cancellationToken)
            {
                var user = await _userRepository.GetAsync(x => x.Id == request.UserId);
                if (user == null)
                {
                    throw new BusinessException("Kullanıcı bulunamadı.");
                }
                var order = await _orderRepository.GetAsync(x => x.Id == request.OrderId);
                if (order == null)
                {
                    throw new BusinessException("Sipariş bulunamadı.");
                }
                if (user.Role != "admin")
                {
                    order.Status = "İptal et";
                }
            }
        }
    }
}
