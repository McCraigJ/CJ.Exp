using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CJ.Exp.DomainInterfaces;
using CJ.Exp.ServiceModels;
using CJ.Exp.ServiceModels.AuthTokens;

namespace CJ.Exp.BusinessLogic.Auth
{
  public class AuthTokenCache : IAuthTokenCache
  {
    private readonly Dictionary<string, DateTime> _tokenCache;

    public List<BusinessErrorSM> BusinessErrors { get; }
    public bool BusinessStateValid { get; }

    public AuthTokenCache()
    {
      BusinessErrors = new List<BusinessErrorSM>();
      BusinessStateValid = true;
      _tokenCache = new Dictionary<string, DateTime>();
    }

    public bool HasAuthToken(string token)
    {
      if (_tokenCache.TryGetValue(token, out DateTime date))
      {
        // remove token if its expired
        if (date <= DateTime.Now)
        {
          _tokenCache.Remove(token);
        }
        else
        {
          return true;
        }
      } 

      return false;
    }

    public void AddAuthToken(AuthTokenSM authToken)
    {
      if (_tokenCache.ContainsKey(authToken.Token))
      {
        BusinessErrors.Add(new BusinessErrorSM(BusinessErrorCodes.DataAlreadyExists, "Token already exists"));
      }
      else
      {
        _tokenCache.Add(authToken.Token, authToken.Expiry);
      }
    }

    public void RemoveAuthToken(string token)
    {
      if (_tokenCache.ContainsKey(token))
      {
        _tokenCache.Remove(token);
      }
      else
      {
        BusinessErrors.Add(new BusinessErrorSM(BusinessErrorCodes.DataNotFound, "Token not found"));
      }
    }

  }
}
