using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using CJ.Exp.Data.Models;
using CJ.Exp.ServiceModels;
using CJ.Exp.ServiceModels.Auth;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using CJ.Exp.BusinessLogic.Interfaces;
using CJ.Exp.Data;
using CJ.Exp.ServiceModels.Roles;
using Microsoft.Extensions.Logging;

namespace CJ.Exp.BusinessLogic.Auth
{
  public class AuthService : ServiceBase, IAuthService
  {
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public AuthService(
      UserManager<ApplicationUser> userManager,
      SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager, ExpDbContext data) : base(data)
    {
      _userManager = userManager;
      _signInManager = signInManager;
      _roleManager = roleManager;
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

    public async Task<AuthResultSM> AddToRole(string userName, string role)
    {
      var user = await _userManager.FindByNameAsync(userName);
      if (user != null)
      {
        var addToRoleResult = await _userManager.AddToRoleAsync(user, role);
        return AuthResultFactory.CreateResultFromIdentityResult(addToRoleResult);
      }
      else
      {
        return AuthResultFactory.CreateUserNotFoundResult();
      }
    }

    public async Task<AuthResultSM> SeedData()
    {
      try
      {


        foreach (var roleName in ApplicationRoles.AllRoles())
        {
          var exists = await _roleManager.RoleExistsAsync(roleName);
          if (!exists)
          {
            var role = new IdentityRole();
            role.Name = roleName;
            await _roleManager.CreateAsync(role);
          }
        }

        var adminUser = new UserSM
        {
          Email = "admin@admin.com",
          FirstName = "Admin",
          LastName = "Admin"
        };

        var result = await RegisterUserAsync(adminUser, "Qwe!23");
        if (result.Succeeded)
        {
          var user = await _userManager.FindByNameAsync(adminUser.Email);
          await _userManager.AddToRoleAsync(user, ApplicationRoles.RoleAdmin);
          return AuthResultFactory.CreateGenericSuccessResult();
        }
        else
        {
          return AuthResultFactory.CreateGenericFailResult(result.Errors.FirstOrDefault()?.Description);
        }
      }
      catch (Exception ex)
      {
        return AuthResultFactory.CreateGenericFailResult(ex.Message); 
      }
    }

    public IQueryable<UserSM> GetUsers()
    {
      return (from u in _data.Users
        select u).ProjectTo<UserSM>();
    }

    public async Task<UserSM> AddUser(UserSM user, string password)
    {
      var currentUser = await _userManager.FindByNameAsync(user.Email);
      if (currentUser != null)
      {
        AddBusinessError(BusinessErrorCodes.DataAlreadyExists, "User already exists");
        return null;
      }

      var appUser = Mapper.Map<ApplicationUser>(user);
       var result = await _userManager.CreateAsync(appUser, password);
      if (result.Succeeded)
      {        
        var newUser = await _userManager.FindByNameAsync(user.Email);
        user.Id = newUser.Id;
        return user;
      }

      foreach (var e in result.Errors)
      {
        AddBusinessError(BusinessErrorCodes.Generic, e.Description);
      }
      
      return null;
    }

    public async Task<UserSM> UpdateUser(UserSM user)
    {
      var currentUser = await _userManager.FindByNameAsync(user.Email);
      if (currentUser == null)
      {
        AddBusinessError(BusinessErrorCodes.DataNotFound, "User cannot be found");
        return null;
      }
      
      currentUser.FirstName = user.FirstName;
      currentUser.LastName = user.LastName;
      var result = await _userManager.UpdateAsync(currentUser);
      if (result.Succeeded)
      {        
        return user;
      }
      AddBusinessError(BusinessErrorCodes.Generic, "User could not be updated");
      return null;
    }

    public async Task<bool> UpdatePassword(UserSM user, string oldPassword, string newPassword)
    {
      var currentUser = await _userManager.FindByNameAsync(user.Email);
      if (currentUser != null)
      {
        AddBusinessError(BusinessErrorCodes.DataAlreadyExists, "User already exists");
        return false;
      }

      var result = await _userManager.ChangePasswordAsync(currentUser, oldPassword, newPassword);

      if (result.Succeeded)
      {
        return true;
      }
      
      AddBusinessError(BusinessErrorCodes.CouldNotUpdate, result.Errors.FirstOrDefault()?.Description);
      return false;
    }

    public async Task DeleteUser(UserSM user)
    {
      var currentUser = await _userManager.FindByNameAsync(user.Email);
      if (currentUser == null)
      {
        AddBusinessError(BusinessErrorCodes.DataNotFound, "User cannot be found");
        return;
      }

      var result = await _userManager.DeleteAsync(currentUser);
      if (result.Succeeded)
      {
        return;
      }
      AddBusinessError(BusinessErrorCodes.Generic, "User could not be removed");
    }

    public async Task UpdateUserRoles(UserSM user, List<string> roles)
    {
      var currentUser = await _userManager.FindByNameAsync(user.Email);
      if (currentUser == null)
      {
        AddBusinessError(BusinessErrorCodes.DataNotFound, "User cannot be found");
        return;
      }

      var currentRoles = (from ur in _data.UserRoles
                          join r in _data.Roles on ur.RoleId equals r.Id
                          where ur.UserId == currentUser.Id
                          select r.Name).ToList();

      await _userManager.RemoveFromRolesAsync(currentUser, currentRoles);

      foreach (var role in roles)
      {
        await _userManager.AddToRoleAsync(currentUser, role);
      }

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
