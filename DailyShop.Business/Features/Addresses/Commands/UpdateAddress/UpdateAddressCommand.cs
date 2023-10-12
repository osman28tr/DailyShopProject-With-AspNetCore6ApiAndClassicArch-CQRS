using AutoMapper;
using DailyShop.Business.Features.Addresses.Dtos;
using DailyShop.Business.Services.Repositories;
using DailyShop.Entities.Concrete;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyShop.Business.Features.Addresses.Commands.UpdateAddress
{
	public class UpdateAddressCommand:IRequest<UpdatedAddressDto>
	{
        public int AppUserId { get; set; }
        public string Title { get; set; }
		public string Description { get; set; }
		public string Adres { get; set; }
		public string City { get; set; }
		public string Country { get; set; }
		public string ZipCode { get; set; }
		public class UpdateAddressCommandHandler : IRequestHandler<UpdateAddressCommand, UpdatedAddressDto>
		{
			private readonly IAddressRepository _addressRepository;
			private readonly IMapper _mapper;

			public UpdateAddressCommandHandler(IAddressRepository addressRepository, IMapper mapper)
			{
				_addressRepository = addressRepository;
				_mapper = mapper;
			}

			public async Task<UpdatedAddressDto> Handle(UpdateAddressCommand request, CancellationToken cancellationToken)
			{
				Address address = await _addressRepository.GetAsync(a => a.AppUserId == request.AppUserId && a.Title == request.Title);
				_mapper.Map(request, address);
				var updatedAddress = await _addressRepository.UpdateAsync(address);
				UpdatedAddressDto updatedAddressDto = _mapper.Map<UpdatedAddressDto>(updatedAddress);
				return updatedAddressDto;
			}
		}
	}
}
