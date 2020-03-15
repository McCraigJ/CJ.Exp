using CJ.Exp.API.Extensions;
using CJ.Exp.ApiModels;
using CJ.Exp.DomainInterfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

namespace CJ.Exp.API.Controllers
{
  [Route("api/users/[action]")]
  public class UsersController : ControllerBase
  {
    private readonly IAuthService _authService;
    private readonly IConfiguration _configuration;
    private readonly IAuthTokenService _authTokenService;

    public UsersController(
        IAuthService authService,
        IConfiguration configuration,
        IAuthTokenService authTokenService,
        ILanguage language
        ) : base(language)
    {
      _authService = authService;
      _configuration = configuration;
      _authTokenService = authTokenService;
    }


    [HttpPost]
    public async Task<IActionResult> Login([FromBody] LoginAM model)
    {
      var result = await _authService.AuthenticateAsync(model.Email, model.Password, false, false);

      if (result.Succeeded)
      {
        var user = await _authService.FindByEmailAsync(model.Email);

        var tokens = await _authTokenService.GenerateAndRegisterAccessAndRefreshTokensAsync(
          user,
          _configuration["JwtKey"],
          Convert.ToInt32(_configuration["JwtExpireHours"]),
          _configuration["JwtIssuer"],
          Convert.ToInt32(_configuration["RefreshTokenExpireHours"]));

        return SuccessResponse(new LoginResponseAM
        {
          Id = user.Id,
          Email = user.Email,
          Token = tokens.Item1,
          RefreshToken = tokens.Item2,
          FirstName = user.FirstName
        });

      }

      return BusinessErrorResponse("INVALID_LOGIN_ATTEMPT");
    }

    [HttpPost]
    public async Task<IActionResult> RefreshToken([FromForm] RefreshTokenAM model)
    {

      var refreshedTokens = await _authTokenService.RefreshAndRegisterTokensAsync(model.CurrentToken, model.RefreshToken, _configuration["JwtKey"],
        Convert.ToInt32(_configuration["JwtExpireHours"]),
        _configuration["JwtIssuer"],
        Convert.ToInt32(_configuration["RefreshTokenExpireHours"]));

      if (refreshedTokens == null)
      {
        return BusinessErrorResponse("CannotRefreshToken");
      }

      return SuccessResponse(
        new RefreshTokenResponseAM
        {
          Token = refreshedTokens.Item1,
          RefreshToken = refreshedTokens.Item2
        });
    }

    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> Logout()
    {
      var token = HttpContext.GetAuthorisationToken();
      if (token != null)
      {
        await _authTokenService.DeleteAuthAndRefreshTokensAsync(token, _configuration["JwtKey"]);
      }

      return SuccessResponse();
    }
  }
}