using Havillah.ApplicationServices.Interfaces;
using Havillah.Core.Domain;
using Havillah.Shared;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace Havillah.ApplicationServices.Authentication.UseCases.Commands;

public class RegisterUserUseCaseCommand: IRequest<Result<string>>
{
    public string UserName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string MiddleName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string PhoneNumber { get; set; }
    public string Address { get; set; }

    public class RegisterUserUseCaseCommandHandler: IRequestHandler<RegisterUserUseCaseCommand, Result<string>>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly IRepository<ApplicationUser> _repository;

        public RegisterUserUseCaseCommandHandler(UserManager<ApplicationUser> userManager, IConfiguration configuration, IRepository<ApplicationUser> repository)
        {
            _userManager = userManager;
            _configuration = configuration;
            _repository = repository;
        }

        public async Task<Result<string>> Handle(RegisterUserUseCaseCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if(user != null) return Result.Fail<string>("User already exist");
            var applicationUser = new ApplicationUser {
                Email = request.Email,
                FirstName = request.FirstName,
                MiddleName = request.MiddleName,
                LastName = request.LastName,
                UserName = request.Email,
                NormalizedUserName = request.Email.ToUpper(),
                PhoneNumber = request.PhoneNumber,
                Address = request.Address
            };
            PasswordHasher<ApplicationUser> ph = new PasswordHasher<ApplicationUser>();
            applicationUser.PasswordHash = ph.HashPassword(applicationUser, request.Password);
            await _repository.Add(applicationUser);
            var result = await _repository.Save();
            return result < 0 ? Result.Fail<string>("Unable to create user") : Result.Ok<string>("User created successfully");
        }
    }
}