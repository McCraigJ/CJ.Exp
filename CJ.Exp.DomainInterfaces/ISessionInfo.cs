using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using CJ.Exp.ServiceModels.Users;

namespace CJ.Exp.DomainInterfaces
{
  public interface ISessionInfo
  {
    UserSM User { get; set; }


    //ClaimsPrincipal CurrentClaimsPrincipal { get; set; }

    //IIdentity CurrentIdentity { get; set; }
  }
}
