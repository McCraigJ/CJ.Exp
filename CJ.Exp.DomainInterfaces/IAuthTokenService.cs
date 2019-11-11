using System;
using System.Collections.Generic;
using System.Text;
using CJ.Exp.ServiceModels;
using CJ.Exp.ServiceModels.AuthTokens;
using CJ.Exp.ServiceModels.Users;

namespace CJ.Exp.DomainInterfaces
{
  public interface IAuthTokenService : IBusinessErrors
  {
    bool HasAuthToken(string token);

    string GenerateAndRegisterToken(UserSM user, string securityKey, int expiryHours, string issuer);

    void AddAuthToken(AuthTokenSM authToken);

    void DeleteAuthToken(string token);
  }
}
