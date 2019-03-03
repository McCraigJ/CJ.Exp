using CJ.Exp.BusinessLogic;
using CJ.Exp.BusinessLogic.Auth;
using CJ.Exp.BusinessLogic.Interfaces;
using CJ.Exp.Data.DataAccess;
using CJ.Exp.Data.DataAccess.CJ.Exp.BusinessLogic;
using CJ.Exp.Data.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace CJ.Exp
{
  public static class CommonStartup
  {
    public static void AddCommonServices(IServiceCollection services)
    {
      services.AddTransient<IAuthService, AuthService>();
      services.AddTransient<IExpensesService, ExpensesService>();

      services.AddTransient<IUsersData, UserData>();
      services.AddTransient<IExpensesData, ExpensesData>();

    }
  }
}
