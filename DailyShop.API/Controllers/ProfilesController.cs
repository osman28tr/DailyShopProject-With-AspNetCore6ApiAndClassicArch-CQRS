using Core.CrossCuttingConcerns.Exceptions;
using Core.Security.Entities;
using DailyShop.API.Helpers;
using DailyShop.Business.Features.Addresses.Commands.DeleteAddress;
using DailyShop.Business.Features.Addresses.Commands.UpdateAddress;
using DailyShop.Business.Features.Addresses.Dtos;
using DailyShop.Business.Features.Addresses.Queries.GetListAddressByUserId;
using DailyShop.Business.Features.AppUsers.Commands.BlockUser;
using DailyShop.Business.Features.AppUsers.Commands.UpdateUser;
using DailyShop.Business.Features.AppUsers.Dtos;
using DailyShop.Business.Features.AppUsers.Queries.GetUser;
using DailyShop.Business.Features.Auths.Dtos;
using DailyShop.Business.Services.AuthService;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DailyShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfilesController : BaseTokenController
    {
        private readonly IAuthService _authService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ImageHelper _imageHelper;

        public ProfilesController(IAuthService authService, IWebHostEnvironment webHostEnvironment, ImageHelper imageHelper)
        {
            _authService = authService;
            _webHostEnvironment = webHostEnvironment;
            _imageHelper = imageHelper;
        }
        [HttpGet("GetUser")]
        public async Task<IActionResult> GetUser()
        {
            int userId = _authService.VerifyToken(GetToken());
            var user = await Mediator.Send(new GetUserQuery() { UserId = userId });

            if (user.ProfileImage != null)
            {
                user.ProfileImage = GetImageByHelper(user.ProfileImage);
            }

            if (user == null)
                throw new BusinessException("Kullanıcı bulunamadı.");

            return Ok(new { data = user, Message = "Kullanıcı bilgileri başarıyla getirildi." });
        }
        [HttpGet("GetListAddressByUserId")]
        public async Task<IActionResult> GetListAddressByUserId([FromQuery] int id)
        {
            var address = await Mediator.Send(new GetListAddressByUserIdQuery { Id = id });
            return Ok(address);
        }
        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromForm] UpdatedUserDto updatedUserDto)
        {
            int userId = _authService.VerifyToken(GetToken());

            updatedUserDto.ProfileImage = await AddUserImageToFile(updatedUserDto.ProfileImageFile);

            UpdateUserCommand updateUserCommand = new() { UpdatedUserDto = updatedUserDto, Id = userId };
            await Mediator.Send(updateUserCommand);

            return Ok(new { Message = "Kullanıcı Bilgileriniz Başarıyla Güncellendi." });
        }

        [HttpPut("UpdateAddress")]
        public async Task<IActionResult> UpdateAddress([FromBody] AddressDto addressDto)
        {
            int userId = _authService.VerifyToken(GetToken());
            UpdateAddressCommand updateAddressCommand = new() { AddressDto = addressDto, UserId = userId, AddressId = addressDto.Id };
            var address = await Mediator.Send(updateAddressCommand);
            return Ok(new { data = address, Message = "Adresiniz Başarıyla Güncellendi." });
        }
        [HttpDelete("DeleteAddress")]
        public async Task<IActionResult> DeleteAddress([FromQuery] int addressId)
        {
            int userId = _authService.VerifyToken(GetToken());
            await Mediator.Send(new DeleteAddressCommand() { UserId = userId, AddressId = addressId });
            return Ok(new { Message = "Adresiniz başarıyla silindi." });
        }
        [HttpGet("Block")]
        public async Task<IActionResult> Block()
        {
            int id = _authService.VerifyToken(GetToken());
            BlockedUserDto blockedUserDto = new() { Id = id };
            string message = await Mediator.Send(new BlockUserCommand { BlockedUserDto = blockedUserDto });
            return Ok(new { Message = message });
        }
        private async Task<string> AddUserImageToFile(IFormFile imageFile)
        {
            string imageName = new String(Path.GetFileNameWithoutExtension(imageFile.FileName).Take(10).ToArray()).Replace(' ', '-');
            imageName = imageName + DateTime.Now.ToString("yymmssfff") + Path.GetExtension(imageFile.FileName);
            var imagePath = Path.Combine(_webHostEnvironment.ContentRootPath, "wwwroot/UserImages", imageName);
            using (var fileStream = new FileStream(imagePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(fileStream);
            }
            return imageName;
        }
        [NonAction]
        public string GetImageByHelper(string imageName)
        {
            string getImage = _imageHelper.GetImage(Request.Scheme, Request.Host, Request.PathBase, imageName);
            return getImage;
        }
    }
}
