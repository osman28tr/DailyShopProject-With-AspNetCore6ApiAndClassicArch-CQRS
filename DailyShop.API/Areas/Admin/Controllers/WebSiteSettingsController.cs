using DailyShop.API.Helpers;
using DailyShop.Business.Features.WebSiteSettings.Commands.UpdateWebSiteSetting;
using DailyShop.Business.Features.WebSiteSettings.Dtos;
using DailyShop.Business.Features.WebSiteSettings.Queries.GetWebSiteSetting;
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
        private readonly ImageHelper _imageHelper;

        public WebSiteSettingsController(IWebHostEnvironment webHostEnvironment, ImageHelper imageHelper)
        {
            _webHostEnvironment = webHostEnvironment;
            _imageHelper = imageHelper;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
			var result = await Mediator.Send(new GetWebSiteSettingQuery());
            if (result != null)
            {
                result.SiteIcon = GetImageByHelper(result.SiteIcon);
                return Ok(new { data = result, message = "Site ayarları başarıyla getirildi." });
            }
            return Ok(null);
        }
        [HttpPut]
        public async Task<IActionResult> Update([FromForm] UpdatedWebSiteSettingDto updatedWebSiteSettingDto)
        {
			string siteIconPath = null;

			if (updatedWebSiteSettingDto.SiteIcon != null)
			    siteIconPath = await AddWebSiteImageToFile(updatedWebSiteSettingDto.SiteIcon);

            string result = await Mediator.Send(new UpdateWebSiteSettingCommand()
            { UpdatedWebSiteSettingDto = updatedWebSiteSettingDto, SiteIconPath = siteIconPath });

            return Ok(new { Message = result });
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
        [NonAction]
		public string GetImageByHelper(string imageName)
		{
			string getImage = _imageHelper.GetImage(Request.Scheme, Request.Host, Request.PathBase, imageName , "WebSiteIcons");
			return getImage;
		}
	}
}
