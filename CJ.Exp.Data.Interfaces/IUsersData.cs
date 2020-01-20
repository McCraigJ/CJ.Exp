using CJ.Exp.ServiceModels;
using CJ.Exp.ServiceModels.Users;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CJ.Exp.Data.Interfaces
{
  public interface IUsersData
  {
    Task<List<UserSM>> GetUsersAsync();

    Task<GridResultSM<UserSM>> GetUsersAsync(UsersFilterSM filter);

    Task<UserSM> GetUserByIdAsync(string id);
  }
}
