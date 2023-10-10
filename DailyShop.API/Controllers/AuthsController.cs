using Core.Security.Dtos;
using Core.Security.JWT;
using DailyShop.Business.Features.Auths.Commands.LoginUser;
using DailyShop.Business.Features.Auths.Commands.RegisterUser;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DailyShop.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthsController : ControllerBase
	{
		private readonly IMediator _mediator;
		public AuthsController(IMediator mediator)
		{
			_mediator = mediator;
		}
		[HttpPost("Register")]
		public async Task<IActionResult> Register(UserForRegisterDto userForRegisterDto)
		{
			RegisterUserCommand registerUserCommand = new()
			{
				UserForRegisterDto = userForRegisterDto,
				IpAddress = GetIpAddress()
			};
			var result = await _mediator.Send(registerUserCommand);

			SetAccessTokenToCookie(result.AccessToken);

			return Ok(result.AccessToken);
		}
		[HttpPost("Login")]
		public async Task<IActionResult> Login(UserForLoginDto userForLoginDto)
		{
			LoginUserCommand loginUserCommand = new() { UserForLoginDto = userForLoginDto, IpAddress = GetIpAddress() };
			var result = await _mediator.Send(loginUserCommand);
			SetAccessTokenToCookie(result.AccessToken);
			return Ok(result.AccessToken);
		}
		private void SetAccessTokenToCookie(AccessToken accessToken)
		{
			CookieOptions cookieOptions = new() { HttpOnly = true, Expires = DateTime.Now.AddDays(1) };
			Response.Cookies.Append("accessToken", accessToken.Token, cookieOptions);
		}
		private string? GetIpAddress()
		{
			if (Request.Headers.ContainsKey("X-Forwarded-For")) return Request.Headers["X-Forwarded-For"];
			return HttpContext.Connection.RemoteIpAddress?.MapToIPv4().ToString();
		}
	}
}
