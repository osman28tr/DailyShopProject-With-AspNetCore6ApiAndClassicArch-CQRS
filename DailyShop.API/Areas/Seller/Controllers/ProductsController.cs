using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DailyShop.API.Areas.Seller.Controllers
{
    [Route("api/Seller/[controller]")]
    [Area("Seller")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetList()
        {
            return Ok();
        }
    }
}
