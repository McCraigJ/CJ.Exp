using System.Security.Claims;
using CJ.Exp.DomainInterfaces;

namespace CJ.Exp.Core
{
  public class ServiceInfo : IServiceInfo
  {
    public ClaimsPrincipal CurrentClaimsPrincipal { get; set; }
  }
}
