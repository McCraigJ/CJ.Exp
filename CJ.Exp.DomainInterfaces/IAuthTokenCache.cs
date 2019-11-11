using System;
using System.Collections.Generic;
using System.Text;
using CJ.Exp.ServiceModels.AuthTokens;

namespace CJ.Exp.DomainInterfaces
{
  public interface IAuthTokenCache : IBusinessErrors
  {
    bool HasAuthToken(string token);
    void AddAuthToken(AuthTokenSM authToken);
    void RemoveAuthToken(string token);
  }
}
