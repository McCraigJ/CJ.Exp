using System.Security.Claims;
using System.Security.Principal;
using CJ.Exp.DomainInterfaces;
using CJ.Exp.ServiceModels.Users;

namespace CJ.Exp.Core
{
  public class SessionInfo : ISessionInfo
  {
    public UserSM User { get; set; }
  }
}
