using CJ.Exp.ServiceModels.Users;
using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using CJ.Exp.ServiceModels;
using Microsoft.AspNetCore.Identity;

namespace CJ.Exp.Auth.EFIdentity
{
  public static class EFAuthResultFactory
  {
    public static AuthResultSM CreateResultFromIdentityResult(IdentityResult result)
    {
      return new AuthResultSM
      {
        Succeeded = result.Succeeded,
        Errors = Mapper.Map<List<ProcessingErrorSM>>(result.Errors)
      };
    }

    public static AuthResultSM CreateResultFromSignInResult(SignInResult result)
    {
      return new AuthResultSM
      {
        Succeeded = result.Succeeded,
        IsLockedOut = result.IsLockedOut
      };
    }
  }
}
