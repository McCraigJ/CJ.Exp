using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CJ.Exp.ServiceModels.Users;

namespace CJ.Exp.DomainInterfaces
{
  public interface IUsersService
  {
    IQueryable<UserSM> GetUsers();

  }
}
