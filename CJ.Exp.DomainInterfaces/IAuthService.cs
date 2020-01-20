using System;
using System.Collections.Generic;
using CJ.Exp.ServiceModels.Users;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CJ.Exp.ServiceModels;

namespace CJ.Exp.DomainInterfaces
{
  public interface IAuthService : IBusinessErrors
  {
    bool IsSignedIn(ClaimsPrincipal principal);
    Task<AuthResultSM> AuthenticateAsync(string userName, string password, bool isPersistent, bool lockoutOnFailure);
    Task SignInAsync(UserSM user);
    Task<AuthResultSM> RegisterUserAsync(UserSM user, string password);
    Task<string> GeneratePasswordResetTokenAsync(string email);
    Task<AuthResultSM> ResetPasswordAsync(string email, string code, string newPassword);
    Task SignOutAsync();
    Task<UserSM> FindByEmailAsync(string email);
    Task<UserSM> GetUserByPrincipalAsync(ClaimsPrincipal principal);
    Task<AuthResultSM> UpdateCurrentUser(ClaimsPrincipal principal, UserSM user);
    Task<AuthResultSM> ChangePasswordAsync(ClaimsPrincipal principal, string oldPassword, string newPassword);    
    Task<AuthResultSM> UpdateRoleAsync(string userName, string role);
    Task<AuthResultSM> SeedData();
    Task<List<UserSM>> GetUsersAsync();
    Task<GridResultSM<UserSM>> GetUsersAsync(UsersFilterSM filter);
    Task<UserSM> GetUserByIdAsync(string id);
    Task<UserSM> AddUserAsync(UserSM user, string password);
    Task<UserSM> UpdateUserAsync(UserSM user);
    Task<bool> UpdatePasswordAsync(UserSM user, string oldPassword, string newPassword);
    Task DeleteUserAsync(UserSM user);
    Task UpdateUserRolesAsync(UserSM user, string role);
    Task<string> GetUserRoleAsync(UserSM user);
    Task<string> GetUserRoleForLoggedInUserAsync(ClaimsPrincipal principal);

  }
}
