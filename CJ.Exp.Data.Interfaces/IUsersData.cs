using CJ.Exp.ServiceModels;
using CJ.Exp.ServiceModels.Users;
using System.Collections.Generic;
using System.Linq;

namespace CJ.Exp.Data.Interfaces
{
  public interface IUsersData
  {
    IQueryable<UserSM> GetUsers();

    //IQueryable<string> GetCurrentUserRoles(string userId);
  }
}
