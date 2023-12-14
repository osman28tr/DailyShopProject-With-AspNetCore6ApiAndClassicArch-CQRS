using Core.CrossCuttingConcerns.Exceptions;
using DailyShop.Business.Services.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyShop.Business.Features.Addresses.Commands.DeleteAddress
{
    public class DeleteAddressCommand:IRequest
    {
        public int AddressId { get; set; }
        public int UserId { get; set; }
        public class DeleteAddressCommandHandler : IRequestHandler<DeleteAddressCommand>
        {
            private readonly IAddressRepository _addressRepository;
            public DeleteAddressCommandHandler(IAddressRepository addressRepository)
            {
                _addressRepository = addressRepository;
            }
            public async Task Handle(DeleteAddressCommand request, CancellationToken cancellationToken)
            {
                var address = await _addressRepository.Query().FirstOrDefaultAsync(x => x.Id == request.AddressId && x.AppUserId == request.UserId);
                if (address != null)
                {
                    await _addressRepository.DeleteAsync(address, false);
                    return;
                }
                throw new BusinessException("Adres silinemedi.");
            }
        }
    }
}
