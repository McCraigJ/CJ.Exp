using System.Threading.Tasks;
using CJ.Exp.DomainInterfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace CJ.Exp.Admin
{
  public static class ServiceInfoInjectorExtensions
  {
    public static void AddServiceInfo(this IApplicationBuilder app)
    {
      app.UseMiddleware<ServiceInfoInjectorMiddleware>();
    }
  }

  public class ServiceInfoInjectorMiddleware
  {
    private readonly RequestDelegate _next;

    public ServiceInfoInjectorMiddleware(RequestDelegate next)
    {
      _next = next;
    }

    public async Task Invoke(HttpContext ctxt, IServiceInfo serviceInfo)
    {
      serviceInfo.CurrentClaimsPrincipal = ctxt.User;

      await _next.Invoke(ctxt);
    }
  }
}
