using AutoMapper;
using DailyShop.Business.Features.Addresses.Dtos;
using DailyShop.Business.Services.Repositories;
using DailyShop.Entities.Concrete;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyShop.Business.Features.Addresses.Queries.GetListAddressByUserId
{
	public class GetListAddressByUserIdQuery:IRequest<List<AddressListByUserIdDto>>
	{
        public int Id { get; set; }
		public class GetListAddressByUserIdQueryHandler : IRequestHandler<GetListAddressByUserIdQuery, List<AddressListByUserIdDto>>
		{
			private readonly IAddressRepository _addressRepository;
			private readonly IMapper _mapper;

			public GetListAddressByUserIdQueryHandler(IAddressRepository addressRepository, IMapper mapper)
			{
				_addressRepository = addressRepository;
				_mapper = mapper;
			}

			public async Task<List<AddressListByUserIdDto>> Handle(GetListAddressByUserIdQuery request, CancellationToken cancellationToken)
			{
				List<Address> addresses = await _addressRepository.Query().Where(a => a.AppUserId == request.Id).Include(t => t.AppUser).ToListAsync();
				var addressListDto = _mapper.Map<List<AddressListByUserIdDto>>(addresses);
				return addressListDto;
			}
		}
	}
}
