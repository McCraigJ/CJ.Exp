using AutoMapper.QueryableExtensions;
using CJ.Exp.Data.Interfaces;
using CJ.Exp.ServiceModels.Users;
using System.Collections.Generic;
using System.Linq;

namespace CJ.Exp.Data.EF.DataAccess
{
  public class UsersDataEfEf : DataEfAccessBase, IUsersData
  {
    public UsersDataEfEf(ExpDbContext data) : base(data)
    {
    }

    public IQueryable<string> GetCurrentUserRoles(string userId)
    {
      return (from ur in _data.UserRoles
       join r in _data.Roles on ur.RoleId equals r.Id
       where ur.UserId == userId
       select r.Name);
    }

    public List<UserSM> GetUsers()
    {
      return GetUsersAsQueryable().ToList();
    }

    public UserSM GetUserById(string id)
    {
      return GetUsersAsQueryable().SingleOrDefault(x => x.Id == id);      
    }

    private IQueryable<UserSM> GetUsersAsQueryable()
    {
      return (from u in _data.Users
        select u).ProjectTo<UserSM>();
    }
  }
}
