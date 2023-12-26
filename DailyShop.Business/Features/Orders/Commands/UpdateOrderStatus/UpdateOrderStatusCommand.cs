using Core.CrossCuttingConcerns.Exceptions;
using DailyShop.Business.Services.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyShop.Business.Features.Orders.Commands.UpdateOrderStatus
{
    public class UpdateOrderStatusCommand:IRequest
    {
        public int? OrderId { get; set; }
        public string? Status { get; set; }
        public class UpdateOrderStatusCommandHandler : IRequestHandler<UpdateOrderStatusCommand>
        {
            private readonly IOrderRepository _orderRepository;
            public UpdateOrderStatusCommandHandler(IOrderRepository orderRepository)
            {
                _orderRepository = orderRepository;
            }
            public async Task Handle(UpdateOrderStatusCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    var order = await _orderRepository.GetAsync(x => x.Id == request.OrderId);
                    if (order == null)
                    {
                        throw new BusinessException("Sipariş bulunamadı.");
                    }
                    order.Status = request.Status;
                    await _orderRepository.UpdateAsync(order);
                }
                catch (Exception hata)
                {
                    throw new BusinessException(hata.Message);
                }
            }
        }
    }
}
