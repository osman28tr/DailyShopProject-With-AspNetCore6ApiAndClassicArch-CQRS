using AutoMapper;
using Core.CrossCuttingConcerns.Exceptions;
using DailyShop.Business.Features.Orders.Dtos;
using DailyShop.Business.Services.Repositories;
using DailyShop.Entities.Concrete;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyShop.Business.Features.Orders.Commands.InsertOrder
{
    public class InsertOrderCommand : IRequest
    {
        public int UserId { get; set; }
        public InsertedOrderDto InsertedOrderDto { get; set; }
        public class InsertOrderCommandHandler : IRequestHandler<InsertOrderCommand>
        {
            private readonly IOrderRepository _orderRepository;
            private readonly IProductRepository _productRepository;
            private readonly IAddressRepository _addressRepository;
            private readonly IMapper _mapper;

            public InsertOrderCommandHandler(IOrderRepository orderRepository, IAddressRepository addressRepository,IProductRepository productRepository ,IMapper mapper)
            {
                _orderRepository = orderRepository;
                _addressRepository = addressRepository;
                _productRepository = productRepository;
                _mapper = mapper;
            }
            public async Task Handle(InsertOrderCommand request, CancellationToken cancellationToken)
            {
                Random generator = new Random();

                var address = await _addressRepository.GetAsync(x => x.Id == request.InsertedOrderDto.AdressId);
                if (address == null)
                    throw new BusinessException("Böyle bir adres bulunamadı.");

                OrderAddress orderAddress = new() { Title = address.Title, Adres = address.Adres, City = address.City, Country = address.Country };
                
                String orderNumber = generator.Next(0, 1000000).ToString("D6");

                var mappedPayment = _mapper.Map<Payment>(request.InsertedOrderDto.InsertedCreditCardDto);
                mappedPayment.UserId = request.UserId;

                Order order = new()
                {
                    UserId = request.UserId,
                    IsPaymentCompleted = true,
                    Status = "New",
                    OrderAddress = orderAddress,
                    OrderNumber = orderNumber,
                    Payment = mappedPayment
                };

                order.TotalPrice = 0;

                foreach (var orderItemDto in request.InsertedOrderDto.InsertedOrderItemDtos)
                {
                    var product = await _productRepository.GetAsync(x => x.Id == orderItemDto.ProductId);
                    orderItemDto.Price = product?.Price * orderItemDto.Quantity;
                    OrderItem orderItem = new()
                    {
	                    ProductId = orderItemDto.ProductId, Color = orderItemDto.Color, Size = orderItemDto.Size, Quantity = orderItemDto.Quantity, Price = orderItemDto.Price
                    };
                    order.TotalPrice += orderItem.Price;
                    order.OrderItems?.Add(orderItem);
                }
                await _orderRepository.AddAsync(order);
            }
        }
    }
}
