using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace CJ.Exp.DomainInterfaces
{
  public interface IServiceInfo
  {
    ClaimsPrincipal CurrentClaimsPrincipal { get; set; }
  }
}
