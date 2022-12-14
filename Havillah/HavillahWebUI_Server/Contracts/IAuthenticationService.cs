using Havillah.Shared;

namespace HavillahWebUI_Server.Contracts;

public interface IAuthenticationService
{
    Task<AuthenticationResult> Login(LoginDto loginModel);
    Task Logout();
}