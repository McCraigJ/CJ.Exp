using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CJ.Exp.ServiceModels;
using CJ.Exp.ServiceModels.AuthTokens;
using CJ.Exp.ServiceModels.Users;

namespace CJ.Exp.DomainInterfaces
{
  public interface IAuthTokenService : IBusinessErrors
  {
    bool HasAuthToken(string token);

    //string GenerateAccessToken(UserSM user, string securityKey, int expiryHours, string issuer);

    //string GenerateRefreshToken();

    //void AddAuthToken(AuthTokenSM authToken);

    Task DeleteAuthAndRefreshTokensAsync(string authToken, string securityKey);

    //void UpdateUserRefreshToken(string userId, string refreshToken);

    //string GetRefreshToken(string userId);

    Task<Tuple<string, string>> RefreshAndRegisterTokensAsync(string currentToken, string refreshToken, string securityKey,
      int expiryHours, string issuer, int refreshTokenHours);

    Tuple<string, string> GenerateAndRegisterAccessAndRefreshTokens(UserSM user, string securityKey, int expiryHours, string issuer, int refreshTokenExpiry);

  }
}
