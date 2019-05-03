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

namespace CJ.Exp.API.Controllers
{
  [Route("api/users")]
  public class UsersController : Controller
  {
    private readonly IAuthService _authService;    
    private readonly IConfiguration _configuration;

    public UsersController(
        IAuthService authService,
        IConfiguration configuration
        )
    {
      _authService = authService;      
      _configuration = configuration;
    }

    [HttpGet]
    public IEnumerable<string> Test()
    {
      return new string[] { "value1", "value2" };
    }

    [HttpPost]
    public async Task<IActionResult> Login([FromBody] LoginAM model)
    {      
      var result = await _authService.AuthenticateAsync(model.Email, model.Password, false, false);

      if (result.Succeeded)
      {
        var user = await _authService.FindByEmailAsync(model.Email);
        var token = GenerateJwtToken(model.Email, user);
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
    public async Task<object> Register([FromBody] RegisterAM model)
    {
      var user = Mapper.Map<UserSM>(model);

      var result = await _authService.RegisterUserAsync(user, model.Password);      

      if (result.Succeeded)
      {
        await _authService.SignInAsync(user);
        return GenerateJwtToken(model.Email, user);
      }

      throw new ApplicationException("UNKNOWN_ERROR");
    }

    private string GenerateJwtToken(string email, UserSM user)
    {
      var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id)
            };

      var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtKey"]));
      var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
      var expires = DateTime.Now.AddDays(Convert.ToDouble(_configuration["JwtExpireDays"]));

      var token = new JwtSecurityToken(
          _configuration["JwtIssuer"],
          _configuration["JwtIssuer"],
          claims,
          expires: expires,
          signingCredentials: creds
      );

      return new JwtSecurityTokenHandler().WriteToken(token);
    }
    
  }
}