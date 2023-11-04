using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DailyShop.API.Helpers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseTokenController : BaseController
    {
        protected string GetToken()
        {
            var authorization = Request.Headers.Authorization;
            var token = authorization.ToString().Split(" ")[1];
            return token;
        }
    }
}
