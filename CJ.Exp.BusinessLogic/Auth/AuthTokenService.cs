using CJ.Exp.Data.Interfaces;
using CJ.Exp.DomainInterfaces;
using CJ.Exp.ServiceModels;
using CJ.Exp.ServiceModels.AuthTokens;
using CJ.Exp.ServiceModels.Users;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Edm.Library.Expressions;

namespace CJ.Exp.BusinessLogic.Auth
{
  public class AuthTokenService : IAuthTokenService
  {
    private readonly IAuthTokensData _authTokensData;
    private readonly IAuthTokenCache _authTokenCache;
    private readonly IAuthService _authService;
    public List<BusinessErrorSM> BusinessErrors { get; set; }
    public bool BusinessStateValid { get; set; }

    public AuthTokenService(IAuthTokensData authTokensData, IAuthTokenCache authTokenCache, IAuthService authService)
    {
      _authTokensData = authTokensData;
      _authTokenCache = authTokenCache;
      _authService = authService;
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

    private AuthTokenSM GenerateAccessToken(UserSM user, string securityKey, int expiryHours, string issuer)
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

      return new AuthTokenSM
      {
        Token = tokenString,
        Expiry = expires,
        Issued = now
      };
    }

    private string GenerateRefreshToken()
    {
      var randomNumber = new byte[32];
      using (var rng = RandomNumberGenerator.Create())
      {
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
      }
    }


    public async Task<Tuple<string, string>> RefreshAndRegisterTokensAsync(string currentToken, string refreshToken, string securityKey, 
      int expiryHours, string issuer, int refreshTokenHours)
    {
      var principal = GetPrincipalFromToken(currentToken, securityKey, false);

      if (principal.Identity is ClaimsIdentity identity)
      {
        var email = identity.FindFirst("sub").Value;

        var user = await _authService.FindByEmailAsync(email);

        if (user != null)
        {
          var savedRefreshToken = _authTokensData.GetRefreshTokenForUserId(user.Id);

          if (savedRefreshToken.RefreshToken == refreshToken)
          {
            return GenerateAndRegisterAccessAndRefreshTokens(user, securityKey, expiryHours, issuer, refreshTokenHours);
          }
        }

      }

      return null;
    }

    private ClaimsPrincipal GetPrincipalFromToken(string token, string securityKey, bool validateLifetime)
    {
      var tokenValidationParameters = new TokenValidationParameters
      {
        ValidateAudience = false, //you might want to validate the audience and issuer depending on your use case
        ValidateIssuer = false,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey)),
        ValidateLifetime = false //here we are saying that we don't care about the token's expiration date
      };

      var tokenHandler = new JwtSecurityTokenHandler();
      var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var securityToken);
      var jwtSecurityToken = securityToken as JwtSecurityToken;
      if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
        throw new SecurityTokenException("Invalid token");

      return principal;
    }


    public Tuple<string, string> GenerateAndRegisterAccessAndRefreshTokens(UserSM user, string securityKey, int expiryHours, string issuer, int refreshTokenHours)
    {
      var authToken = GenerateAccessToken(user, securityKey, expiryHours, issuer);

      _authTokenCache.AddAuthToken(authToken);
      _authTokensData.AddAuthToken(authToken);

      
      _authTokensData.DeleteRefreshTokenForUser(user.Id);

      var refreshToken = new RefreshTokenSM
      {
        Expiry = DateTime.Now.AddHours(refreshTokenHours),
        RefreshToken = GenerateRefreshToken(),
        UserId = user.Id
      };

      _authTokensData.AddRefreshToken(refreshToken);

      return new Tuple<string, string>(authToken.Token, refreshToken.RefreshToken);
    }
    
    public void AddAuthToken(AuthTokenSM authToken)
    {
      _authTokensData.AddAuthToken(authToken);
      _authTokenCache.AddAuthToken(authToken);
    }

    public async Task DeleteAuthAndRefreshTokensAsync(string token, string securityKey)
    {
      _authTokensData.DeleteAuthToken(token);
      _authTokenCache.RemoveAuthToken(token);

      var principal = GetPrincipalFromToken(token, securityKey, true);

      if (principal != null)
      {
        var user = await _authService.GetUserByPrincipalAsync(principal);

        _authTokensData.DeleteRefreshTokenForUser(user.Id);
      }
    }
  }
}
