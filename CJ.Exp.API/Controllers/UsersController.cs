using AutoMapper;
using CJ.Exp.API.ApiModels;
using CJ.Exp.DomainInterfaces;
using CJ.Exp.ServiceModels.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using CJ.Exp.API.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

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
        IAuthTokenService authTokenService
        )
    {
      _authService = authService;      
      _configuration = configuration;
      _authTokenService = authTokenService;
    }
    

    [HttpPost]
    public async Task<IActionResult> Login([FromForm] LoginAM model)
    {      
      var result = await _authService.AuthenticateAsync(model.Email, model.Password, false, false);

      if (result.Succeeded)
      {
        var user = await _authService.FindByEmailAsync(model.Email);

        var tokens =_authTokenService.GenerateAndRegisterAccessAndRefreshTokens(
          user,
          _configuration["JwtKey"],
          Convert.ToInt32(_configuration["JwtExpireHours"]),
          _configuration["JwtIssuer"],
          Convert.ToInt32(_configuration["RefreshTokenExpireHours"]));

        return SuccessResponse(new
        {
          id = user.Id,
          email = user.Email,          
          token = tokens.Item1,
          refreshToken = tokens.Item2
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

      return SuccessResponse(refreshedTokens != null
        ? new
        {
          token = refreshedTokens.Item1,
          refreshToken = refreshedTokens.Item2
        }
        : null);
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