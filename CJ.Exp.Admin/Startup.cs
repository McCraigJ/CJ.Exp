using System;
using AutoMapper;
using CJ.Exp.BusinessLogic;
using CJ.Exp.BusinessLogic.Auth;
using CJ.Exp.Core;
//using CJ.Exp.Auth.EFIdentity;
using CJ.Exp.Data.EF;
using CJ.Exp.Data.EF.DataModels;
using CJ.Exp.Data.Interfaces;
using CJ.Exp.Data.MongoDb.DataAccess;
using CJ.Exp.Data.MongoDb.DataModels;
using CJ.Exp.Data.MongoDb.Interfaces;
using CJ.Exp.Data.MongoDb.Mongo;
using CJ.Exp.DomainInterfaces;
using CJ.Exp.LanguageProvider;
using CJ.Exp.Notification;
using CJ.Exp.ServiceModels.Users;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;


namespace CJ.Exp.Admin
{
  public class Startup
  {
    private readonly IHostingEnvironment _env;
    public Startup(IConfiguration configuration, IHostingEnvironment env)
    {
      Configuration = configuration;
      _env = env;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      IApplicationSettings appSettings = new ApplicationSettings();

      Configuration.Bind(appSettings);

      services.AddSingleton(appSettings);
      services.AddSingleton<IAppMongoClient, AppMongoClient>();

      ILanguage languageProvider = new TextCache();
      languageProvider.Initialise(_env.IsDevelopment());

      services.AddSingleton(languageProvider);

      services.AddIdentity<ApplicationUserMongo, ApplicationRoleMongo>()
        .AddMongoDbStores<ApplicationUserMongo, ApplicationRoleMongo, Guid>
        (
          appSettings.ConnectionString, appSettings.DatabaseName
        )
        .AddDefaultTokenProviders();

      // Add application services.
      services.AddScoped<INotification, EmailSender>();
      CommonStartup.AddCommonServices(services);

      services.AddScoped<IAuthService, AuthService<ApplicationUserMongo, ApplicationRoleMongo>>();

      services.AddScoped<IExpensesData, ExpensesDataMongo>();

      services.AddScoped<ISessionInfo, SessionInfo>();

      services.AddSession(options =>
      {
        // Set a short timeout for easy testing.
        //options.IdleTimeout = TimeSpan.FromSeconds(10);
        options.Cookie.HttpOnly = true;
        // Make the session cookie essential
        options.Cookie.IsEssential = true;
      });

      services.AddAutoMapper();
      services.AddMvc();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseBrowserLink();
        app.UseDeveloperExceptionPage();
        app.UseDatabaseErrorPage();
      }
      else
      {
        app.UseExceptionHandler("/Home/Error");
      }

      app.UseStaticFiles();

      app.UseSession();

      app.UseAuthentication();

      app.UseMiddleware<SessionInfoMiddleware>();

      app.UseMvc(routes =>
      {
        routes.MapRoute(
                  name: "default",
                  template: "{controller=Home}/{action=Index}/{id?}");
      });
    }
  }
}
