using Core.CrossCuttingConcerns.Exceptions;
using DailyShop.API.Helpers;
using DailyShop.Business.Features.AppUsers.Commands.BlockUser;
using DailyShop.Business.Features.AppUsers.Dtos;
using DailyShop.Business.Features.AppUsers.Queries.GetListUser;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DailyShop.API.Areas.Admin.Controllers
{
    [Route("api/Admin/[controller]")]
    [Area("Admin")]
    [ApiController]
    [Authorize]
    public class UserSettingsController : BaseController
    {
        private readonly ImageHelper _imageHelper;
        public UserSettingsController(ImageHelper imageHelper)
        {
            _imageHelper = imageHelper;
        }
        [HttpGet("Index")]
        public async Task<IActionResult> Index()
        {
            var users = await Mediator.Send(new GetListUserQuery());
            if (users == null)
                throw new BusinessException("Herhangi bir kullanıcı bulunamadı.");
            foreach (var user in users)
            {
                if (user.ProfileImage != null)
                {
                    user.ProfileImage = GetImageByHelper(user.ProfileImage);
                }
            }
            return Ok(new { Message = "Kullanıcılar başarıyla getirildi.", data = users });
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> BlockUser()
        {
            BlockedUserDto blockedUserDto = new() { Id = int.Parse(HttpContext.GetRouteData().Values["id"].ToString()) };
            var message = await Mediator.Send(new BlockUserCommand { BlockedUserDto = blockedUserDto });
            return Ok(new { Message = message });
        }
        [NonAction]
        public string GetImageByHelper(string imageName)
        {
            string getImage = _imageHelper.GetImage(Request.Scheme, Request.Host, Request.PathBase, imageName);
            return getImage;
        }
    }
}
