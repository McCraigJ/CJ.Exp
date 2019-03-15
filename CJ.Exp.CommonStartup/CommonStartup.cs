using CJ.Exp.BusinessLogic.Auth;
using CJ.Exp.BusinessLogic.Expenses;
using CJ.Exp.Data.EF.DataAccess;
using CJ.Exp.Data.EF.DataAccess.CJ.Exp.BusinessLogic;
using CJ.Exp.Data.Interfaces;
using CJ.Exp.DomainInterfaces;
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
