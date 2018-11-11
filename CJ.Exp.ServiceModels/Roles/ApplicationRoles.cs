using System;
using System.Collections.Generic;
using System.Text;

namespace CJ.Exp.ServiceModels.Roles
{
  public static class ApplicationRoles
  {
    public static string RoleAdmin = "Admin";
    public static string RoleUser = "User";

    public static List<string> AllRoles()
    {
      return new List<string>
      {
        "Admin", "User"        
      };
    }    
  }
}
