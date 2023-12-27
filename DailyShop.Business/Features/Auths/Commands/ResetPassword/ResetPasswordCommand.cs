using Core.CrossCuttingConcerns.Exceptions;
using Core.Security.Hashing;
using DailyShop.Business.Features.Auths.Dtos;
using DailyShop.Business.Services.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Linq.Dynamic.Core.Tokenizer;
using System.Text;
using System.Threading.Tasks;

namespace DailyShop.Business.Features.Auths.Commands.ResetPassword
{
    public class ResetPasswordCommand:IRequest
    {
        public ResetPasswordDto? ResetPasswordDto { get; set; }
        public string? Token { get; set; }
        public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand>
        {
            private readonly IUserRepository _userRepository;
            public ResetPasswordCommandHandler(IUserRepository userRepository)
            {
                _userRepository = userRepository;
            }
            public async Task Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
            {
                var handler = new JwtSecurityTokenHandler();
                var jsonToken = handler.ReadToken(request.Token) as JwtSecurityToken;

                if (jsonToken != null)
                {
                    var emailClaim = jsonToken.Claims.FirstOrDefault(claim => claim.Type == "email");

                    if (emailClaim != null)
                    {
                        string email = emailClaim.Value;
                        var user = await _userRepository.GetAsync(x => x.Email == email);
                        byte[] passwordHash, passwordSalt;
                        HashingHelper.CreatePasswordHash(request.ResetPasswordDto.Password, out passwordHash, out passwordSalt);
                        user.PasswordSalt = passwordSalt;
                        user.PasswordHash = passwordHash;
                        user.UpdatedAt = DateTime.Now;
                        await _userRepository.UpdateAsync(user);
                    }
                    else
                    {
                        throw new BusinessException("Email bilgisi bulunamadı.");
                    }
                }
                else
                {
                    throw new BusinessException("Geçersiz veya süresi dolmuş bir token. Lütfen tekrar token alıp deneyin.");
                }
            }
        }
    }
}
