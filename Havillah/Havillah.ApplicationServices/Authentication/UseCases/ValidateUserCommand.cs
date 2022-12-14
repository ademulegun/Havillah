using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Havillah.Core.Domain;
using Havillah.Shared;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace Havillah.ApplicationServices.Authentication.UseCases;

public class ValidateUserCommand: IRequest<Result<string>>
{
    public string Email { get; set; }
    public string Password { get; set; }
    public bool RememberMe { get; set; }
    
    public class ValidateUserCommandHandler: IRequestHandler<ValidateUserCommand, Result<string>>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;
        public ValidateUserCommandHandler(UserManager<ApplicationUser> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task<Result<string>> Handle(ValidateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null && !await _userManager.CheckPasswordAsync(user, request.Password)) return Result.Fail<string>("Invalid username or password");
            {
                var issuer = _configuration["Jwt:Issuer"];
                var audience = _configuration["Jwt:Audience"];
                var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[]
                    {
                        new Claim("Id", user.Id.ToString()),
                        new Claim(JwtRegisteredClaimNames.Sub, request.Email),
                        new Claim(JwtRegisteredClaimNames.Email, request.Email),
                        new Claim(JwtRegisteredClaimNames.Jti, user.Id.ToString())
                    }),
                    Expires = DateTime.UtcNow.AddMinutes(5),
                    Issuer = issuer,
                    Audience = audience,
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
                };
                var tokenHandler = new JwtSecurityTokenHandler();
                var token = tokenHandler.CreateToken(tokenDescriptor);
                var stringToken = tokenHandler.WriteToken(token);
                return Result.Ok<string>(stringToken);
            }
        }
    }
}