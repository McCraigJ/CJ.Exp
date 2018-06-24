using AutoMapper;
using CJ.Exp.Admin.Services;
using CJ.Exp.Data;
using CJ.Exp.Data.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CJ.Exp.Admin
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
      services.AddDbContext<ExpDbContext>(options =>
          options.UseSqlServer(Configuration.GetConnectionString("CJ.Exp.ConnectionString"), b => b.MigrationsAssembly("CJ.Exp.Data")));

      services.AddIdentity<ApplicationUser, IdentityRole>()
          .AddEntityFrameworkStores<ExpDbContext>()
          .AddDefaultTokenProviders();

      // Add application services.
      services.AddTransient<IEmailSender, EmailSender>();
      CommonStartup.AddCommonServices(services);            

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

      app.UseAuthentication();

      app.UseMvc(routes =>
      {
        routes.MapRoute(
                  name: "default",
                  template: "{controller=Home}/{action=Index}/{id?}");
      });
    }
  }
}
