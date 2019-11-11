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
  public class UsersController : Controller
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

    [HttpGet]
    [Authorize]
    public IEnumerable<string> Test()
    {
      return new string[] { "value1", "value2" };
    }

    [HttpPost]
    public async Task<IActionResult> Login([FromForm] LoginAM model)
    {      
      var result = await _authService.AuthenticateAsync(model.Email, model.Password, false, false);

      if (result.Succeeded)
      {
        var user = await _authService.FindByEmailAsync(model.Email);

        var token =_authTokenService.GenerateAndRegisterToken(
          user,
          _configuration["JwtKey"],
          Convert.ToInt32(_configuration["JwtExpireDays"]),
          _configuration["JwtIssuer"]);

        return Ok(new
        {
          id = user.Id,
          email = user.Email,          
          token = token
        });

      }

      throw new ApplicationException("INVALID_LOGIN_ATTEMPT");
    }

    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> Logout()
    {
      //await _authService.SignOutAsync();

      var token = HttpContext.GetAuthorisationToken();
      if (token != null)
      {
        _authTokenService.DeleteAuthToken(token);
      }
      return Ok();
    }

    [HttpPost]
    public async Task<object> Register([FromBody] RegisterAM model)
    {
      var user = Mapper.Map<UserSM>(model);

      var result = await _authService.RegisterUserAsync(user, model.Password);      

      if (result.Succeeded)
      {
        //await _authService.SignInAsync(user);

        return _authTokenService.GenerateAndRegisterToken(
          user,
          _configuration["JwtKey"],
          Convert.ToInt32(_configuration["JwtExpireDays"]),
          _configuration["JwtIssuer"]);
      }

      throw new ApplicationException("UNKNOWN_ERROR");
    }

  }
}