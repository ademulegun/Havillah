using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Havillah.ApplicationServices.Authentication.Dto;
using Havillah.ApplicationServices.Common.Options;
using Havillah.ApplicationServices.Interfaces;
using Havillah.Core.Domain;
using Havillah.Shared;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace Havillah.ApplicationServices.Authentication.UseCases.Queries;

public class ValidateUserUseCaseCommand: IRequest<Result<Token>>
{
    public string Email { get; set; }
    public string Password { get; set; }
    public bool RememberMe { get; set; }
    
    public class ValidateUserCommandHandler: IRequestHandler<ValidateUserUseCaseCommand, Result<Token>>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly IRepository<ApplicationUser> _repository;
        private readonly JwtSettings _jwtSettings;
        public ValidateUserCommandHandler(UserManager<ApplicationUser> userManager, IConfiguration configuration, IRepository<ApplicationUser> repository,
            IOptionsSnapshot<JwtSettings> jwtSettings)
        {
            _jwtSettings = jwtSettings.Value;
            _userManager = userManager;
            _configuration = configuration;
            _repository = repository;
        }

        public async Task<Result<Token>> Handle(ValidateUserUseCaseCommand request, CancellationToken cancellationToken)
        {
            var user = await _repository.Find(x=>x.Email == request.Email.Trim());
            var set = _configuration.GetSection("Jwt").GetSection("Issuer");
            if (user == null && !await _userManager.CheckPasswordAsync(user, request.Password)) return Result.Fail<Token>("Invalid username or password");
            {
                var issuer = _jwtSettings.Issuer;
                var audience = _jwtSettings.Audience;
                var key = Encoding.ASCII.GetBytes(_jwtSettings.Key);
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
                return Result.Ok<Token>(new Token()
                {
                    Expiration = DateTime.UtcNow.AddMinutes(5).ToString(),
                    TokenValue = stringToken
                });
            }
        }
    }
}