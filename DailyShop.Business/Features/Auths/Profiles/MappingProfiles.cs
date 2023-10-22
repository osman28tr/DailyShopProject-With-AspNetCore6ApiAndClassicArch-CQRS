using AutoMapper;
using Core.Security.Dtos;
using DailyShop.Business.Features.Auths.DailyFrontends;
using DailyShop.Business.Features.Auths.Dtos;
using DailyShop.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyShop.Business.Features.Auths.Profiles
{
    public class MappingProfiles:Profile
	{
        public MappingProfiles()
        {
    //        CreateMap<UserForLoginDto, UserForLoginDto>()
				////.ForMember(c => c.email, opt => opt.MapFrom(c => c.Email))
    ////            .ForMember(c => c.password, opt => opt.MapFrom(c => c.Password))
				//.ReverseMap();

			CreateMap<UserForRegisterDto, UserForRegisterFrontendDto>()
				.ForMember(c => c.email, opt => opt.MapFrom(c => c.Email))
				.ForMember(c => c.password, opt => opt.MapFrom(c => c.Password))
				//.ForMember(c => c.confirmpassword, opt => opt.MapFrom(c => c.ConfirmPassword))
				.ForMember(c => c.phonenumber, opt => opt.MapFrom(c => c.PhoneNumber))
				.ForMember(c => c.name, opt => opt.MapFrom(c => c.Name))
				.ForMember(c => c.surname, opt => opt.MapFrom(c => c.Surname))
				//.ForMember(c => c.role, opt => opt.MapFrom(c => c.Role))
				.ReverseMap();

			CreateMap<Address, AddressDto>()
                //.ForMember(c => c.id, opt => opt.MapFrom(c => c.Id))
                //.ForMember(c => c.title, opt => opt.MapFrom(c => c.Title))
                //.ForMember(c => c.description, opt => opt.MapFrom(c => c.Description))
                //.ForMember(c => c.address, opt => opt.MapFrom(c => c.Adres))
                //.ForMember(c => c.city, opt => opt.MapFrom(c => c.City))
                //.ForMember(c => c.country, opt => opt.MapFrom(c => c.Country))
                //.ForMember(c => c.zipcode, opt => opt.MapFrom(c => c.ZipCode))
                .ReverseMap();

            CreateMap<AppUser, LoggedUserDto>()
                //.ForMember(c => c.id, opt => opt.MapFrom(c => c.Id))
                //.ForMember(c=>c.name,opt=>opt.MapFrom(c=>c.FirstName))
                //.ForMember(c=>c.surname,opt=>opt.MapFrom(c=>c.LastName))
                //.ForMember(c=>c.email,opt=>opt.MapFrom(c=>c.Email))
                //.ForMember(c=>c.phone,opt=>opt.MapFrom(c=>c.PhoneNumber))
                //.ForMember(c=>c.role,opt=>opt.MapFrom(c=>c.Role))
                //.ForMember(c=>c.createdAt,opt=>opt.MapFrom(c=>c.CreatedAt))
                //.ForMember(c=>c.updatedAt,opt=>opt.MapFrom(c=>c.UpdatedAt))
                //.ForMember(c=>c.profileImage,opt=>opt.MapFrom(c=>c.ProfileImage))
                .ReverseMap();   
        }
    }
}
