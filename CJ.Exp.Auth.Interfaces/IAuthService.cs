using CJ.Exp.ServiceModels;
using CJ.Exp.ServiceModels.Auth;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CJ.Exp.Auth.Interfaces
{
  public interface IAuthService
  {
    bool IsSignedIn(ClaimsPrincipal principal);
    Task<AuthResultSM> AuthenticateAsync(string userName, string password);
    Task SignInAsync(UserSM user);
    Task<AuthResultSM> RegisterUserAsync(UserSM user, string password);
    Task<string> GeneratePasswordResetTokenAsync(string email);
    Task<AuthResultSM> ResetPasswordAsync(string email, string code, string newPassword);
    Task SignOutAsync();
    Task<UserSM> FindByEmailAsync(string email);
    Task<UserSM> GetUserByPrincipalAsync(ClaimsPrincipal principal);
    Task<AuthResultSM> UpdateCurrentUser(ClaimsPrincipal principal, UserSM user);
    Task<AuthResultSM> ChangePasswordAsync(ClaimsPrincipal principal, string oldPassword, string newPassword);
  }
}
