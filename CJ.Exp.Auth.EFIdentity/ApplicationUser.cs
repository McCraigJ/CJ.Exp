using CJ.Exp.DomainInterfaces;
using Microsoft.AspNetCore.Identity;

namespace CJ.Exp.Auth.EFIdentity
{
  public class ApplicationUser : IdentityUser, IApplicationUser
  {
    public string ApplicationId
    {
      get => Id;
      set => Id = value;
    }

    public string FirstName { get; set; }
    public string LastName { get; set; }

  }
}
