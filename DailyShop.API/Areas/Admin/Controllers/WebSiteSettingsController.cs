using DailyShop.API.Helpers;
using DailyShop.Business.Features.WebSiteSettings.Commands.UpdateWebSiteSetting;
using DailyShop.Business.Features.WebSiteSettings.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DailyShop.API.Areas.Admin.Controllers
{
    [Route("api/Admin/[controller]")]
    [Area("Admin")]
    [ApiController]
    [Authorize]
    public class WebSiteSettingsController : BaseController
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public WebSiteSettingsController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdatedWebSiteSettingDto updatedWebSiteSettingDto)
        {
            //string siteIconPath = await AddWebSiteImageToFile(updatedWebSiteSettingDto.Icon);

            //string result = await Mediator.Send(new UpdateWebSiteSettingCommand()
            //    { UpdatedWebSiteSettingDto = updatedWebSiteSettingDto, SiteIconPath = siteIconPath });

            return Ok("new { message = result }");
        }
        private async Task<string> AddWebSiteImageToFile(IFormFile imageFile)
        {
            string imageName = new String(Path.GetFileNameWithoutExtension(imageFile.FileName).Take(10).ToArray()).Replace(' ', '-');
            imageName = imageName + DateTime.Now.ToString("yymmssfff") + Path.GetExtension(imageFile.FileName);
            var imagePath = Path.Combine(_webHostEnvironment.ContentRootPath, "wwwroot/WebSiteIcons", imageName);
            using (var fileStream = new FileStream(imagePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(fileStream);
            }
            return imageName;
        }
    }
}
