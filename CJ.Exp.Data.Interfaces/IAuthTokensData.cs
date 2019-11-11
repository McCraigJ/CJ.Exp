using System;
using System.Collections.Generic;
using System.Text;
using CJ.Exp.ServiceModels.AuthTokens;

namespace CJ.Exp.Data.Interfaces
{
  public interface IAuthTokensData
  {
    AuthTokenSM GetAuthToken(string token);

    void AddAuthToken(AuthTokenSM authToken);

    void DeleteAuthToken(string token);
  }
}
