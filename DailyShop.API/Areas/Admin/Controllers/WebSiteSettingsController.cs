using DailyShop.API.Helpers;
using DailyShop.Business.Features.WebSiteSettings.Commands.UpdateWebSiteSetting;
using DailyShop.Business.Features.WebSiteSettings.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DailyShop.API.Areas.Admin.Controllers
{
    [Route("api/Admin/[controller]")]
    [Area("Admin")]
    [ApiController]
    public class WebSiteSettingsController : BaseController
    {
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdatedWebSiteSettingDto updatedWebSiteSettingDto)
        {
            string result = await Mediator.Send(new UpdateWebSiteSettingCommand() { UpdatedWebSiteSettingDto = updatedWebSiteSettingDto });
            return Ok(new { message = result });
        }
    }
}
