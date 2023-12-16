using AutoMapper;
using Core.CrossCuttingConcerns.Exceptions;
using DailyShop.Business.Features.Payments.Dtos;
using DailyShop.Business.Services.Repositories;
using DailyShop.Entities.Concrete;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyShop.Business.Features.Payments.Commands.InsertPayment
{
    public class InsertPaymentCommand:IRequest
    {
        public InsertedPaymentDto InsertedPaymentDto { get; set; }
        public int UserId { get; set; }
        public class InsertPaymentCommandHandler : IRequestHandler<InsertPaymentCommand>
        {
            private readonly IPaymentRepository _paymentRepository;
            private readonly IMapper _mapper;

            public InsertPaymentCommandHandler(IPaymentRepository paymentRepository, IMapper mapper)
            {
                _paymentRepository = paymentRepository;
                _mapper = mapper;
            }

            public async Task Handle(InsertPaymentCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    var mappedPayment = _mapper.Map<Payment>(request.InsertedPaymentDto);
                    mappedPayment.UserId = request.UserId;
                    await _paymentRepository.AddAsync(mappedPayment);
                }
                catch (Exception hata)
                {
                    throw new BusinessException(hata.Message);
                }
            }
        }
    }
}
