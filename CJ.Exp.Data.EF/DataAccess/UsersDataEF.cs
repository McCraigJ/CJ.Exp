using AutoMapper.QueryableExtensions;
using CJ.Exp.Data.Interfaces;
using CJ.Exp.ServiceModels.Users;
using System.Collections.Generic;
using System.Linq;
using CJ.Exp.ServiceModels;

namespace CJ.Exp.Data.EF.DataAccess
{
  public class UsersDataEf : DataEfAccessBase, IUsersData
  {
    public UsersDataEf(ExpDbContext data) : base(data)
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

    public GridResultSM<UserSM> GetUsers(UsersFilterSM filter)
    {
      throw new System.NotImplementedException();
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
