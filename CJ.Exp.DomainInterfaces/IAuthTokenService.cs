using CJ.Exp.ServiceModels.Users;
using System;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace CJ.Exp.DomainInterfaces
{
  public interface IAuthTokenService : IBusinessErrors
  {
    bool HasAuthToken(string token);

    Task DeleteAuthAndRefreshTokensAsync(string authToken, string securityKey);

    string GetSubClaimValueFromIdentity(IIdentity identity);

    Task<Tuple<string, string>> RefreshAndRegisterTokensAsync(string currentToken, string refreshToken, string securityKey,
      int expiryHours, string issuer, int refreshTokenHours);

    Tuple<string, string> GenerateAndRegisterAccessAndRefreshTokens(UserSM user, string securityKey, int expiryHours, string issuer, int refreshTokenExpiry);

    Task<UserSM> GetUserFromTokenAsync(string token, string securityKey);
  }
}
