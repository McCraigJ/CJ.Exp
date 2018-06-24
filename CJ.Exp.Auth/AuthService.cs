using AutoMapper;
using CJ.Exp.Auth.Interfaces;
using CJ.Exp.Data.Models;
using CJ.Exp.ServiceModels;
using CJ.Exp.ServiceModels.Auth;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CJ.Exp.BusinessLogic.Auth
{
  public class AuthService : IAuthService
  {
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public AuthService(
      UserManager<ApplicationUser> userManager,
      SignInManager<ApplicationUser> signInManager)
    {
      _userManager = userManager;
      _signInManager = signInManager;
    }

    public async Task<AuthResultSM> AuthenticateAsync(string userName, string password, bool isPersistent, bool lockoutOnFailure)
    {
      var result = await _signInManager.PasswordSignInAsync(userName, password, isPersistent, lockoutOnFailure);
      return AuthResultFactory.CreateResultFromSignInResult(result);      
    }

    public async Task<AuthResultSM> ChangePasswordAsync(ClaimsPrincipal principal, string oldPassword, string newPassword)
    {
      var user = await _userManager.GetUserAsync(principal);
      if (user == null)
      {
        return AuthResultFactory.CreateUserNotFoundResult();        
      }

      var changePasswordResult = await _userManager.ChangePasswordAsync(user, oldPassword, newPassword);
      return AuthResultFactory.CreateResultFromIdentityResult(changePasswordResult);      
    }

    public async Task<UserSM> FindByEmailAsync(string email)
    {
      var user = await _userManager.FindByEmailAsync(email);
      return Mapper.Map<UserSM>(user);
    }

    public async Task<string> GeneratePasswordResetTokenAsync(string email)
    {
      var user = await _userManager.FindByEmailAsync(email);
      if (user == null)
      {
        return null;
      }
      return await _userManager.GeneratePasswordResetTokenAsync(user);
    }

    public async Task<UserSM> GetUserByPrincipalAsync(ClaimsPrincipal principal)
    {
      var user = await _userManager.GetUserAsync(principal);
      return Mapper.Map<UserSM>(user);
    }

    public bool IsSignedIn(ClaimsPrincipal principal)
    {
      return _signInManager.IsSignedIn(principal);
    }

    public async Task<AuthResultSM> RegisterUserAsync(UserSM user, string password)
    {
      var appUser = Mapper.Map<ApplicationUser>(user);
      var result = await _userManager.CreateAsync(appUser, password);
      return AuthResultFactory.CreateResultFromIdentityResult(result);      
    }
    

    public async Task<AuthResultSM> ResetPasswordAsync(string email, string code, string newPassword)
    {

      var user = await _userManager.FindByEmailAsync(email);
      if (user == null)
      {
        // Don't reveal that the user does not exist
        return AuthResultFactory.CreateSilentFailResult();
      }

      var result = await _userManager.ResetPasswordAsync(user, code, newPassword);

      return AuthResultFactory.CreateResultFromIdentityResult(result);
    }

    public async Task SignInAsync(UserSM user)
    {
      var appUser = Mapper.Map<ApplicationUser>(user);
      if (appUser != null)
      {
        await _signInManager.SignInAsync(appUser, isPersistent: false);
      }
    }

    public async Task SignOutAsync()
    {
      await _signInManager.SignOutAsync();
    }

    public async Task<AuthResultSM> UpdateCurrentUser(ClaimsPrincipal principal, UserSM userUpdates)
    {
      var user = await _userManager.GetUserAsync(principal);
      if (user == null)
      {
        return AuthResultFactory.CreateUserNotFoundResult();
      }
      
      if (userUpdates.Email != user.Email)
      {
        var setEmailResult = await _userManager.SetEmailAsync(user, userUpdates.Email);
        if (!setEmailResult.Succeeded)
        {
          return AuthResultFactory.CreateResultFromIdentityResult(setEmailResult);
        }
        var setUserNameResult = await _userManager.SetUserNameAsync(user, userUpdates.Email);
        if (!setUserNameResult.Succeeded)
        {
          return AuthResultFactory.CreateResultFromIdentityResult(setEmailResult);
        }
      }

      user.FirstName = userUpdates.FirstName;
      user.LastName = userUpdates.LastName;
      var result = await _userManager.UpdateAsync(user);
      
      return AuthResultFactory.CreateResultFromIdentityResult(result);
    }    
  }
}
