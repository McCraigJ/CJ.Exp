using CJ.Exp.ServiceModels.Expenses;

namespace CJ.Exp.ServiceModels.Users
{
  public class UserSM : ServiceModelBase
  {
    public string Id { get; set; }
    public string Email { get; set; } 
    public string FirstName { get; set; }
    public string LastName { get; set; }
  }

  public class UsersFilterSM : SearchFilterBaseSM { }
}
