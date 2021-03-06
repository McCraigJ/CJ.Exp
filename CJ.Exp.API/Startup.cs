﻿using AutoMapper;
using CJ.Exp.Auth.EFIdentity;
using CJ.Exp.Data.EF;
using CJ.Exp.Data.EF.DataModels;
using CJ.Exp.DomainInterfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using CJ.Exp.API.Extensions;
using CJ.Exp.API.Middleware;
using CJ.Exp.BusinessLogic;
using CJ.Exp.BusinessLogic.Auth;
using CJ.Exp.BusinessLogic.Expenses;
using CJ.Exp.Data.Interfaces;
using CJ.Exp.Data.MongoDb.DataAccess;
using CJ.Exp.ServiceModels.Users;
using CJ.Exp.Data.MongoDb.DataModels;
using CJ.Exp.Data.MongoDb.Interfaces;
using CJ.Exp.Data.MongoDb.Mongo;
using CJ.Exp.LanguageProvider;
using CJ.Exp.Notification;
using CJ.Exp.Core;
using Microsoft.AspNetCore.Http;
using StackExchange.Redis;

namespace CJ.Exp.API
{
  public class Startup
  {
    private readonly IHostingEnvironment _env;

    public Startup(IConfiguration configuration, IHostingEnvironment env)
    {
      _env = env;
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddCors();

      // ===== Add Jwt Authentication ========
      JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear(); // => remove default claims

      services
        .AddAuthentication(options =>
        {
          options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
          options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(cfg =>
        {
          cfg.RequireHttpsMetadata = false;
          cfg.SaveToken = true;
          cfg.TokenValidationParameters = new TokenValidationParameters
          {
            ValidIssuer = Configuration["JwtIssuer"],
            ValidAudience = Configuration["JwtIssuer"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JwtKey"])),
            ClockSkew = TimeSpan.Zero // remove delay of token when expire
          };

          cfg.Events = new JwtBearerEvents
          {
            OnAuthenticationFailed = (context) =>
            {
              if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
              {
                context.Response.Headers.Add("Token-Expired", "true");
              }
              return Task.CompletedTask;
            }
          };
        });

      IApplicationSettings appSettings = new ApplicationSettings();

      Configuration.Bind(appSettings);

      services.AddSingleton(appSettings);
      services.AddSingleton<IAppMongoClient, AppMongoClient>();

      //ILanguage languageProvider = new TextCache();
      //languageProvider.Initialise(_env.IsDevelopment());

      //services.AddSingleton(languageProvider);

      services.AddIdentity<ApplicationUserMongo, ApplicationRoleMongo>()
        .AddMongoDbStores<ApplicationUserMongo, ApplicationRoleMongo, Guid>
        (
          appSettings.ConnectionString, appSettings.DatabaseName
        )
        .AddDefaultTokenProviders();

      // Add application services.
      services.AddScoped<INotification, EmailSender>();

      services.AddScoped<IExpensesService, ExpensesService>();
      services.AddScoped<IUsersData, UsersDataMongo>();

      services.AddScoped<IAuthService, AuthService<ApplicationUserMongo, ApplicationRoleMongo>>();

      services.AddScoped<IExpensesData, ExpensesDataMongo>();

      services.AddScoped<ISessionInfo, SessionInfo>();

      services.AddScoped<IAuthTokenService, AuthTokenService>();

      services.AddSingleton<IAuthTokenCache, AuthTokenCache>();

      services.AddScoped<IAuthTokensData, AuthTokenDataMongo>();


      services.AddAutoMapper();
      
      services.AddMvc();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    {
      //if (env.IsDevelopment())
      //{
      //  app.UseDeveloperExceptionPage();
      //}

      app.UseStaticFiles();

      app.UseStatusCodePages();

      app.UseMiddleware<ExceptionMiddleware>();

      app.UseMiddleware<AuthMiddleware>();

      app.UseMvc();
      
    }
  }
}
