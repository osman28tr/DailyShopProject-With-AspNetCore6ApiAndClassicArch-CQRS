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
using Core.CrossCuttingConcerns.Exceptions;

namespace DailyShop.Business.Features.Addresses.Commands.UpdateAddress
{
    public class UpdateAddressCommand : IRequest<AddressDto?>
    {
        public AddressDto? AddressDto { get; set; }
        public int UserId { get; set; }
        public int AddressId { get; set; }
        public class UpdateAddressCommandHandler : IRequestHandler<UpdateAddressCommand, AddressDto?>
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

            public async Task<AddressDto?> Handle(UpdateAddressCommand request, CancellationToken cancellationToken)
            {
	            var addressDto = request.AddressDto;

	            if (addressDto == null)
		            throw new BusinessException("Adres bulunamadı");

	            var address = await _addressRepository
		            .Query()
		            .Where(a => a.AppUserId == request.UserId && a.Id == request.AddressId)
		            .FirstOrDefaultAsync(cancellationToken: cancellationToken);

	            if (address != null)
	            {
		            _mapper.Map(addressDto, address);
		            address.UpdatedAt = DateTime.Now;
		            await _addressRepository.UpdateAsync(address);
	            }
	            else
	            {
		            var user = await _appUserRepository.GetAsync(u => u.Id == request.UserId) ?? throw new BusinessException("Kullanıcı bulunamadı");

		            var newAddressEntity = new Address
		            {
			            AppUserId = user.Id,
			            CreatedAt = DateTime.Now,
			            UpdatedAt = DateTime.Now,
			            AppUser = user
		            };
		            _mapper.Map(addressDto, newAddressEntity);

		            var addAddress = await _addressRepository.AddAsync(newAddressEntity);

		            return _mapper.Map<AddressDto>(addAddress);
	            }

	            return _mapper.Map<AddressDto>(address);
            }
		}
    }
}
