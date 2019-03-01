//using CJ.Exp.ServiceModels;
//using CJ.Exp.ServiceModels.Auth;
//using Microsoft.AspNetCore.Identity;
//using System.Collections.Generic;
//using AutoMapper;

//namespace CJ.Exp.BusinessLogic.Auth
//{
//  public static class AuthResultFactory
//  {
//    public static AuthResultSM CreateGenericFailResult(string message = null)
//    {
//      return new AuthResultSM
//      {
//        Succeeded = false,
//        Errors = new List<ProcessingErrorSM> { new ProcessingErrorSM { Description = message} }
//      };
//    }

//    public static AuthResultSM CreateGenericSuccessResult()
//    {
//      return new AuthResultSM
//      {
//        Succeeded = true
//      };
//    }

//    public static AuthResultSM CreateUserNotFoundResult()
//    {
//      return new AuthResultSM
//      {
//        Succeeded = false,
//        UserNotFound = true
//      };
//    }

//    public static AuthResultSM CreateSilentFailResult()
//    {
//      return new AuthResultSM
//      {
//        Succeeded = false,
//        SilentFail = true
//      };
//    }

//    public static AuthResultSM CreateResultFromIdentityResult(IdentityResult result)
//    {
//      return new AuthResultSM
//      {
//        Succeeded = result.Succeeded,
//        Errors = Mapper.Map<List<ProcessingErrorSM>>(result.Errors)
//      };
//    }

//    public static AuthResultSM CreateResultFromSignInResult(SignInResult result)
//    {
//      return new AuthResultSM
//      {
//        Succeeded = result.Succeeded,
//        IsLockedOut = result.IsLockedOut
//      };
//    }
//  }
//}
