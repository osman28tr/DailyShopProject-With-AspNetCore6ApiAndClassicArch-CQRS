using DailyShop.API.Helpers;
using DailyShop.Business.Features.WebSiteSettings.Queries.GetWebSiteSetting;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DailyShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AboutsController : BaseController
    {
        private readonly ImageHelper _imageHelper;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public AboutsController(ImageHelper imageHelper, IWebHostEnvironment webHostEnvironment)
        {
            _imageHelper = imageHelper;
            _webHostEnvironment = webHostEnvironment;
        }
        [HttpGet("GetAbout")]
        public async Task<IActionResult> GetAbout()
        {
            var result = await Mediator.Send(new GetWebSiteSettingQuery());
            return Ok(new { Message = "Hakkımızda verileri çekildi.", data = result.About });
        }

        [HttpGet("GetIcon")]
        public async Task<IActionResult> GetIcon()
        {
            var result = await Mediator.Send(new GetWebSiteSettingQuery());
            if (result != null)
            {
                result.SiteIcon = GetImageByHelper(result.SiteIcon);
                return Ok(new { Message = "Site icon bilgisi çekildi.", data = result.SiteIcon });
            }
            return Ok(null);
        }
        [NonAction]
        public string GetImageByHelper(string imageName)
        {
            string getImage = _imageHelper.GetImage(Request.Scheme, Request.Host, Request.PathBase, imageName, "WebSiteIcons");
            return getImage;
        }
    }
}
