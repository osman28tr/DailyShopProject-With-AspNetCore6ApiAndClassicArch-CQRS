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

namespace DailyShop.Business.Features.Addresses.Commands.InsertAddress
{
	public class InsertAddressCommand:IRequest<InsertedAddressDto>
	{
        public int AppUserId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Adres { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }
		public class InsertAddressCommandHandler : IRequestHandler<InsertAddressCommand, InsertedAddressDto>
		{
			private readonly IAddressRepository _addressRepository;
			private readonly IMapper _mapper;

			public InsertAddressCommandHandler(IAddressRepository addressRepository, IMapper mapper)
			{
				_addressRepository = addressRepository;
				_mapper = mapper;
			}

			public async Task<InsertedAddressDto> Handle(InsertAddressCommand request, CancellationToken cancellationToken)
			{
				var mappedAddress = _mapper.Map<Address>(request);
				var address = await _addressRepository.AddAsync(mappedAddress);
				InsertedAddressDto insertedAddressDto = _mapper.Map<InsertedAddressDto>(address);
				return insertedAddressDto;
			}
		}
	}
}
