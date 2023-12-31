﻿using Core.Security.Dtos;
using Core.Security.JWT;
using DailyShop.API.Helpers;
using DailyShop.Business.Features.Auths.Commands.ForgotPassword;
using DailyShop.Business.Features.Auths.Commands.LoginUser;
using DailyShop.Business.Features.Auths.Commands.RegisterUser;
using DailyShop.Business.Features.Auths.Commands.ResetPassword;
using DailyShop.Business.Features.Auths.Dtos;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DailyShop.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthsController : BaseTokenController
	{
		[HttpPost("Register")]
		public async Task<IActionResult> Register(UserForRegisterDto userForRegisterDto)
		{
			RegisterUserCommand registerUserCommand = new()
			{
				UserForRegisterDto = userForRegisterDto,
				IpAddress = GetIpAddress()
			};
			var result = await Mediator.Send(registerUserCommand);

			SetAccessTokenToCookie(result.AccessToken);

			return Ok(new { Message = "Kayıt işlemi başarıyla gerçekleştirildi." });
		}
		[HttpPost("Login")]
		public async Task<IActionResult> Login(UserForLoginDto userForLoginDto)
		{
			LoginUserCommand loginUserCommand = new() { UserForLoginDto = userForLoginDto, IpAddress = GetIpAddress() };
			var result = await Mediator.Send(loginUserCommand);
			SetAccessTokenToCookie(result.AccessToken);
			return Ok(new { authorization = result.AccessToken.Token, user = result.LoggedUserDto, Message = "Başarıyla giriş yaptınız" });
		}
		[HttpPost("ForgotPassword")]
		public async Task<IActionResult> ForgotPassword([FromQuery] string email)
		{
			await Mediator.Send(new ForgotPasswordCommand() { Email = email });
			return Ok(new { Message = "Şifre yenileme linki mail adresinize gönderildi." });
		}

		[HttpPost("ResetPassword")]
		public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto resetPasswordDto)
		{
			var token = GetToken();
			await Mediator.Send(new ResetPasswordCommand() { ResetPasswordDto = resetPasswordDto , Token = token});
			return Ok(new { Message = "Şifreniz başarıyla güncellendi." });
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
