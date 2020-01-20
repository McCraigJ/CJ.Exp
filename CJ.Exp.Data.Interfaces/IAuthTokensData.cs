using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CJ.Exp.ServiceModels.AuthTokens;

namespace CJ.Exp.Data.Interfaces
{
  public interface IAuthTokensData
  {
    Task<AuthTokenSM> GetAuthTokenAsync(string token);

    Task AddAuthTokenAsync(AuthTokenSM authToken);

    Task DeleteAuthTokenAsync(string token);

    Task<RefreshTokenSM> GetRefreshTokenForUserIdAsync(string userId);

    Task AddRefreshTokenAsync(RefreshTokenSM refreshToken);

    Task DeleteRefreshTokenForUserAsync(string userId);
  }
}
