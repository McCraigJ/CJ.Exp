using System;
using System.Collections.Generic;
using System.Text;

namespace CJ.Exp.ServiceModels.Roles
{
  public static class ApplicationRoles
  {
    public static string RoleAdmin = "_admin";
    public static string RoleUser = "_user";

    public static List<string> AllRoles()
    {
      return new List<string> { RoleAdmin, RoleUser};
    }
  }  
}
