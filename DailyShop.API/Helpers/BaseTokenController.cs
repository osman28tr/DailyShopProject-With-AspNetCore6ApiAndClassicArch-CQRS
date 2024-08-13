using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;

namespace DailyShop.API.Helpers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseTokenController : BaseController
    {
        protected string GetToken()
        {
            var authorization = Request.Headers.Authorization;
            if (authorization == StringValues.Empty)
            {
                return null;
            }

            if (!authorization.Contains("Bearer"))
                return authorization;

            var token = authorization.ToString().Split(" ")[1];
            return token;
        }
    }
}
