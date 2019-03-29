using Microsoft.AspNetCore.Identity;
using System;

namespace CJ.Exp.Data.EF.DataModels
{
  public class ApplicationUserEf : IdentityUser
  {
    public string FirstName { get; set; }
    public string LastName { get; set; }

  }
}
