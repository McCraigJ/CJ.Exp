using AutoMapper;
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
using System.Text;
using CJ.Exp.BusinessLogic.Auth;
using CJ.Exp.ServiceModels.Users;

namespace CJ.Exp.API
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddCors();
      services.AddDbContext<ExpDbContext>(options =>
          options.UseSqlServer(Configuration.GetConnectionString("CJ.Exp.ConnectionString"), b => b.MigrationsAssembly("CJ.Exp.Data")));

      services.AddIdentity<ApplicationUser, IdentityRole>()
          .AddEntityFrameworkStores<ExpDbContext>()
          .AddDefaultTokenProviders();


      // ===== Add Jwt Authentication ========
      JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear(); // => remove default claims
      services
          .AddAuthentication(options =>
          {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
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
          });

      services.AddTransient<IAuthService, AuthService>();

      services.AddAutoMapper();
      services.AddMvc();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }

      app.UseStaticFiles();

      app.UseAuthentication();

      app.UseMvc();
    }
  }
}
