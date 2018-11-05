using CJ.Exp.BusinessLogic;
using CJ.Exp.BusinessLogic.Auth;
using CJ.Exp.BusinessLogic.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace CJ.Exp
{
  public static class CommonStartup
  {
    public static void AddCommonServices(IServiceCollection services)
    {
      services.AddTransient<IAuthService, AuthService>();
      services.AddTransient<IExpensesService, ExpensesService>();
    }
  }
}
