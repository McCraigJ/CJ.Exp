using AutoMapper.QueryableExtensions;
using CJ.Exp.Data.Interfaces;
using CJ.Exp.ServiceModels.Users;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CJ.Exp.ServiceModels;
using Microsoft.EntityFrameworkCore;

namespace CJ.Exp.Data.EF.DataAccess
{
  public class UsersDataEf : DataEfAccessBase, IUsersData
  {
    public UsersDataEf(ExpDbContext data) : base(data)
    {
    }

    public async Task<List<UserSM>> GetUsersAsync()
    {
      return await GetUsersAsQueryable().ToListAsync();
    }

    public Task<GridResultSM<UserSM>> GetUsersAsync(UsersFilterSM filter)
    {
      throw new System.NotImplementedException();
    }

    public async Task<UserSM> GetUserByIdAsync(string id)
    {
      return await GetUsersAsQueryable().SingleOrDefaultAsync(x => x.Id == id);      
    }

    private IQueryable<UserSM> GetUsersAsQueryable()
    {
      return (from u in _data.Users
        select u).ProjectTo<UserSM>();
    }
  }
}
