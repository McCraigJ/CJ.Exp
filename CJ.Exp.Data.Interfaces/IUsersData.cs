using System;
using CJ.Exp.ServiceModels;
using CJ.Exp.ServiceModels.Users;
using System.Collections.Generic;
using System.Linq;

namespace CJ.Exp.Data.Interfaces
{
  public interface IUsersData
  {
    List<UserSM> GetUsers();

    GridResultSM<UserSM> GetUsers(UsersFilterSM filter);

    UserSM GetUserById(string id);
    
    //IQueryable<string> GetCurrentUserRoles(string userId);
  }
}
