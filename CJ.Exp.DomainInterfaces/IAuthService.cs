using System;
using System.Collections.Generic;
using CJ.Exp.ServiceModels.Users;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

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
    Task<AuthResultSM> UpdateRole(string userName, string role);
    Task<AuthResultSM> SeedData();
    List<UserSM> GetUsers();
    UserSM GetUserById(string id);
    Task<UserSM> AddUser(UserSM user, string password);
    Task<UserSM> UpdateUser(UserSM user);
    Task<bool> UpdatePassword(UserSM user, string oldPassword, string newPassword);
    Task DeleteUser(UserSM user);
    Task UpdateUserRoles(UserSM user, string role);
    Task<string> GetUserRole(UserSM user);
    Task<string> GetUserRoleForLoggedInUser(ClaimsPrincipal principal);

  }
}
