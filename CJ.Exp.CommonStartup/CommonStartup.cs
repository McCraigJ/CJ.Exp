﻿using System;
using CJ.Exp.BusinessLogic.Expenses;
using CJ.Exp.Data.Interfaces;
using CJ.Exp.Data.MongoDb.DataAccess;
using CJ.Exp.DomainInterfaces;
using Microsoft.Extensions.DependencyInjection;

namespace CJ.Exp
{
  public static class CommonStartup
  {
    public static void AddCommonServices(IServiceCollection services)
    {
      //services.AddTransient<IAuthService, AuthService>();
      services.AddTransient<IExpensesService, ExpensesService>();

      services.AddTransient<IUsersData, UsersDataMongo>();
      //services.AddTransient<IExpensesData, ExpensesDataEf>();

    }
  }
}
