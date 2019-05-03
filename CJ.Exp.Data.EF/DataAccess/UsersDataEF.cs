using AutoMapper.QueryableExtensions;
using CJ.Exp.Data.Interfaces;
using CJ.Exp.ServiceModels.Users;
using System.Collections.Generic;
using System.Linq;

namespace CJ.Exp.Data.EF.DataAccess
{
  public class UsersDataEF : DataAccessBase, IUsersData
  {
    public UsersDataEF(ExpDbContext data) : base(data)
    {
    }

    public IQueryable<string> GetCurrentUserRoles(string userId)
    {
      return (from ur in _data.UserRoles
       join r in _data.Roles on ur.RoleId equals r.Id
       where ur.UserId == userId
       select r.Name);
    }

    public IQueryable<UserSM> GetUsers()
    {
      return (from u in _data.Users
              select u).ProjectTo<UserSM>();
    }
  }
}
