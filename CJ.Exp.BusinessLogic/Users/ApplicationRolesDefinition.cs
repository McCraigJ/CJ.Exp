using System.Collections.Generic;

namespace CJ.Exp.BusinessLogic.Users
{
  public static class ApplicationRolesDefinition
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
