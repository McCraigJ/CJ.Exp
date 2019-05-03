using CJ.Exp.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CJ.Exp.ServiceModels.Users;

namespace CJ.Exp.Data.MongoDb.DataAccess
{
  public class UsersDataMongo : IUsersData
  {
    public IQueryable<UserSM> GetUsers()
    {
      throw new NotImplementedException();
    }

    //public IQueryable<string> GetCurrentUserRoles(string userId)
    //{
    //  throw new NotImplementedException();
    //}
  }
}
