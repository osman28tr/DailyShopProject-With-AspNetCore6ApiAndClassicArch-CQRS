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

namespace DailyShop.Business.Features.Addresses.Commands.DeleteAddress
{
	public class DeleteAddressCommand:IRequest<DeletedAddressDto>
	{
        public int UserId { get; set; }
        public string Title { get; set; }
		public class DeleteAddressCommandHandler : IRequestHandler<DeleteAddressCommand, DeletedAddressDto>
		{
			private readonly IAddressRepository _addressRepository;
			private readonly IMapper _mapper;

			public DeleteAddressCommandHandler(IAddressRepository addressRepository, IMapper mapper)
			{
				_addressRepository = addressRepository;
				_mapper = mapper;
			}

			public async Task<DeletedAddressDto> Handle(DeleteAddressCommand request, CancellationToken cancellationToken)
			{
				Address address = await _addressRepository.GetAsync(a => a.AppUserId == request.UserId && a.Title == request.Title);
				var deletedAddress = await _addressRepository.DeleteAsync(address);
				DeletedAddressDto deletedAddressDto = _mapper.Map<DeletedAddressDto>(deletedAddress);
				return deletedAddressDto;
			}
		}
	}
}
