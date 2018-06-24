using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using CJ.Exp.Admin.Models;
using CJ.Exp.Admin.Models.ManageViewModels;
using CJ.Exp.Admin.Services;
using CJ.Exp.Auth.Interfaces;
using CJ.Exp.ServiceModels;
using CJ.Exp.ServiceModels.Auth;

namespace CJ.Exp.Admin.Controllers
{
  [Authorize]
  [Route("[controller]/[action]")]
  public class ManageController : Controller
  {    
    private readonly IEmailSender _emailSender;
    private readonly ILogger _logger;
    private readonly IAuthService _authService;

    private readonly UrlEncoder _urlEncoder;

    private const string AuthenticatorUriFormat = "otpauth://totp/{0}:{1}?secret={2}&issuer={0}&digits=6";
    private const string RecoveryCodesKey = nameof(RecoveryCodesKey);

    public ManageController(     
    IEmailSender emailSender,
      ILogger<ManageController> logger,
      UrlEncoder urlEncoder,
      IAuthService authService)
    {
      
      _emailSender = emailSender;
      _logger = logger;
      _urlEncoder = urlEncoder;
      _authService = authService;
    }

    [TempData]
    public string StatusMessage { get; set; }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
      var user = await _authService.GetUserByPrincipalAsync(User);
      //var user = await _userManager.GetUserAsync(User);
      if (user == null)
      {
        throw new ApplicationException($"Unable to load user");
      }

      var model = AutoMapper.Mapper.Map<IndexViewModel>(user);
      model.StatusMessage = StatusMessage;      

      return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Index(IndexViewModel model)
    {
      if (!ModelState.IsValid)
      {
        return View(model);
      }
      
      var user = await _authService.GetUserByPrincipalAsync(User);
      if (user == null)
      {
        throw new ApplicationException($"Unable to load user");
      }

      var result = await _authService.UpdateCurrentUser(User, AutoMapper.Mapper.Map<UserSM>(model));      

      if (result.Succeeded)
      {
        StatusMessage = "Your profile has been updated";
      } else
      {
        StatusMessage = "An unexpected error has occurred while updating your data";
      }
      
      return RedirectToAction(nameof(Index));
    }
    
    [HttpGet]
    public IActionResult ChangePassword()
    {      
      var model = new ChangePasswordViewModel { StatusMessage = StatusMessage };
      return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
    {
      if (!ModelState.IsValid)
      {
        return View(model);
      }

      var user = await _authService.GetUserByPrincipalAsync(User);
      
      if (user == null)
      {
        throw new ApplicationException($"Unable to load user");
      }

      var result = await _authService.ChangePasswordAsync(User, model.OldPassword, model.NewPassword);

      if (!result.Succeeded)
      {
        AddErrors(result);
        return View(model);
      }

      await _authService.SignInAsync(user);
      _logger.LogInformation("User changed their password successfully.");
      StatusMessage = "Your password has been changed.";

      return RedirectToAction(nameof(ChangePassword));
    }

    #region Helpers

    private void AddErrors(AuthResultSM result)
    {
      foreach (var error in result.Errors)
      {
        ModelState.AddModelError(string.Empty, error.Description);
      }
    }
    

    #endregion
  }
}
