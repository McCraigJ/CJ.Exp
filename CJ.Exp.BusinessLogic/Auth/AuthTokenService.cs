using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CJ.Exp.Data.Interfaces;
using CJ.Exp.DomainInterfaces;
using CJ.Exp.ServiceModels;
using CJ.Exp.ServiceModels.AuthTokens;
using CJ.Exp.ServiceModels.Users;
using Microsoft.IdentityModel.Tokens;

namespace CJ.Exp.BusinessLogic.Auth
{
  public class AuthTokenService : IAuthTokenService
  {
    private readonly IAuthTokensData _authTokensData;
    private readonly IAuthTokenCache _authTokenCache;
    public List<BusinessErrorSM> BusinessErrors { get; set; }
    public bool BusinessStateValid { get; set; }

    public AuthTokenService(IAuthTokensData authTokensData, IAuthTokenCache authTokenCache)
    {
      _authTokensData = authTokensData;
      _authTokenCache = authTokenCache;
    }

    public bool HasAuthToken(string token)
    {
      if (_authTokenCache.HasAuthToken(token))
      {
        return true;
      }

      var authToken = _authTokensData.GetAuthToken(token);
      if (authToken != null)
      {
        if (authToken.Expiry < DateTime.Now)
        {
          _authTokensData.DeleteAuthToken(token);
        }
        else
        {
          _authTokenCache.AddAuthToken(authToken);
          return true;
        }
      }

      return false;
    }

    public string GenerateAndRegisterToken(UserSM user, string securityKey, int expiryHours, string issuer)
    {
      var now = DateTime.Now;

      var claims = new List<Claim>
      {
        new Claim(JwtRegisteredClaimNames.Sub, user.Email),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        new Claim(ClaimTypes.NameIdentifier, user.Id)
      };

      var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));
      var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
      var expires = now.AddHours(expiryHours);

      var token = new JwtSecurityToken(
        issuer,
        issuer,
        claims,
        expires: expires,
        signingCredentials: creds
      );

      string tokenString = new JwtSecurityTokenHandler().WriteToken(token);

      var authToken = new AuthTokenSM
      {
        Expiry = expires,
        Issued = now,
        Token = tokenString
      };

      _authTokenCache.AddAuthToken(authToken);
      _authTokensData.AddAuthToken(authToken);

      return tokenString;

    }

    public void AddAuthToken(AuthTokenSM authToken)
    {
      _authTokensData.AddAuthToken(authToken);
      _authTokenCache.AddAuthToken(authToken);
    }

    public void DeleteAuthToken(string token)
    {
      _authTokensData.DeleteAuthToken(token);
      _authTokenCache.RemoveAuthToken(token);
    }
  }
}
