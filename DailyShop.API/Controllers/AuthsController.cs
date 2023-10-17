using Core.Security.Dtos;
using Core.Security.JWT;
using DailyShop.Business.Features.Auths.Commands.LoginUser;
using DailyShop.Business.Features.Auths.Commands.RegisterUser;
using DailyShop.Business.Features.Auths.DailyFrontends;
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
		public async Task<IActionResult> Register(UserForRegisterFrontendDto userForRegisterFrontendDto)
		{
			RegisterUserCommand registerUserCommand = new()
			{
				UserForRegisterFrontendDto = userForRegisterFrontendDto,
				IpAddress = GetIpAddress()
			};
			var result = await _mediator.Send(registerUserCommand);

			SetAccessTokenToCookie(result.AccessToken);

			return Ok(new { Message = "Kayıt işlemi başarıyla gerçekleştirildi." });
		}
		[HttpPost("Login")]
		public async Task<IActionResult> Login(UserForLoginFrontendDto userForLoginFrontendDto)
		{
			LoginUserCommand loginUserCommand = new() { UserForLoginFrontendDto = userForLoginFrontendDto, IpAddress = GetIpAddress() };
			var result = await _mediator.Send(loginUserCommand);
			SetAccessTokenToCookie(result.AccessToken);
			return Ok(new { authorization = result.AccessToken.Token, user = result.LoggedUserDto, Message = "Başarıyla giriş yaptınız" });
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
