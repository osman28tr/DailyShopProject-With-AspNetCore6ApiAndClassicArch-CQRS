using Core.CrossCuttingConcerns.Exceptions;
using Core.Security.Dtos;
using DailyShop.Business.Services.Repositories;
using DailyShop.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyShop.Business.Features.Auths.Rules
{
	public class AuthBusinessRules
	{
		private readonly IUserRepository _userRepository;
		private readonly IAppUserRepository _appUserRepository;
		private readonly IAddressRepository _addressRepository;

		public AuthBusinessRules(IUserRepository userRepository, IAppUserRepository appUserRepository, IAddressRepository addressRepository)
		{
			_userRepository = userRepository;
			_appUserRepository = appUserRepository;
			_addressRepository = addressRepository;
		}
		public async Task EmailCanNotBeDuplicatedWhenRegistered(string email)
		{
			AppUser? user = await _appUserRepository.GetAsync(u => u.Email == email);

			if (user != null)
			{
				throw new BusinessException("Email exits already");
			}
		}
		public async Task EmailMustExist(string email)
		{
			AppUser? user = await _appUserRepository.GetAsync(u => u.Email == email);

			if (user == null)
			{
				throw new BusinessException("No registered user for this email.");
			}
		}
		public async Task<List<Address>>? GetAddress(int userId)
		{
			List<Address> address = await _addressRepository.Query().Where(a => a.AppUserId == userId).ToListAsync();
			return address;
		}
	}
}
