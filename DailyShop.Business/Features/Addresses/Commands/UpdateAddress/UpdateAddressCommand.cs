using AutoMapper;
using DailyShop.Business.Features.Addresses.Dtos;
using DailyShop.Business.Features.Auths.Dtos;
using DailyShop.Business.Services.Repositories;
using DailyShop.Entities.Concrete;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyShop.Business.Features.Addresses.Commands.UpdateAddress
{
    public class UpdateAddressCommand : IRequest<List<AddressDto>?>
    {
        public List<AddressDto>? AddressDtos { get; set; }
        public int UserId { get; set; }
        public int AddressId { get; set; }
        public class UpdateAddressCommandHandler : IRequestHandler<UpdateAddressCommand, List<AddressDto>?>
        {
            private readonly IAddressRepository _addressRepository;
            private readonly IAppUserRepository _appUserRepository;
            private readonly IMapper _mapper;

            public UpdateAddressCommandHandler(IAddressRepository addressRepository, IMapper mapper, IAppUserRepository appUserRepository)
            {
                _addressRepository = addressRepository;
                _mapper = mapper;
                _appUserRepository = appUserRepository;
            }

            public async Task<List<AddressDto>?> Handle(UpdateAddressCommand request, CancellationToken cancellationToken)
            {
                List<AddressDto> mappedAddressDto = new List<AddressDto>();
                if (request.AddressDtos != null)
                {
                    List<Address> addresses = await _addressRepository.Query().Where(a => a.AppUserId == request.UserId).ToListAsync();

                    AppUser user = await _appUserRepository.GetAsync(x => x.Id == request.UserId);
                    foreach (var address in addresses)
                    {
                        await _addressRepository.DeleteAsync(address);
                    }
                    List<Address> mappedNewAddress = _mapper.Map<List<Address>>(request.AddressDtos);
                    foreach (var mappedAddress in mappedNewAddress)
                    {
                        mappedAddress.AppUserId = request.UserId;
                        mappedAddress.AppUser = user;
                    }
                    foreach (var newAddress in mappedNewAddress)
                    {
                        await _addressRepository.AddAsync(newAddress);
                    }
                    mappedAddressDto = _mapper.Map<List<AddressDto>>(mappedNewAddress);
                    return mappedAddressDto;
                }
                return null;
            }
        }
    }
}
