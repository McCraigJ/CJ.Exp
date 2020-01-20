using Microsoft.AspNetCore.Identity;

namespace CJ.Exp.Data.EF.DataModels
{
  public class ApplicationUserEf : IdentityUser
  {
    public string FirstName { get; set; }
    public string LastName { get; set; }

  }
}
