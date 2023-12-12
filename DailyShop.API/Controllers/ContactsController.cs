using DailyShop.API.Helpers;
using DailyShop.API.Models;
using DailyShop.Business.Features.WebSiteSettings.Queries.GetWebSiteSetting;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DailyShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : BaseController
    {
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await Mediator.Send(new GetWebSiteSettingQuery());
            if (result != null)
            {
                GetContactViewModel getContact = new()
                    { Email = result.Email, Phone = result.Phone, Address = result.Address };
                return Ok(new { Message = "İletişim verileri çekildi.", data = getContact });
            }
            return Ok(null);
        }
    }
}
