using CJ.Exp.API.Extensions;
using CJ.Exp.DomainInterfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace CJ.Exp.API.Middleware
{
  public class AuthMiddleware
  {
    private readonly RequestDelegate _next;
    private readonly ILogger _logger;

    public AuthMiddleware(RequestDelegate next, ILoggerFactory logger)
    {
      _logger = logger.CreateLogger("Api");
      _next = next;
    }

    public async Task InvokeAsync(HttpContext httpContext, IAuthenticationService authenticationService, IAuthTokenService authTokenService, ISessionInfo sessionInfo, IConfiguration configuration)
    {
      var path = httpContext.Request.Path.Value.ToLowerInvariant();

      if (path.Contains("/users/login") || path.Contains("/users/register") || path.Contains("/users/refreshtoken"))
      {
        await _next(httpContext);
      }
      else
      {
        var result = await authenticationService.AuthenticateAsync(httpContext, JwtBearerDefaults.AuthenticationScheme);

        if (result.Succeeded)
        {
          var token = httpContext.GetAuthorisationToken();
          if (token != null)
          {
            if (await authTokenService.HasAuthTokenAsync(token))
            {

              sessionInfo.User = await authTokenService.GetUserFromTokenAsync(token, configuration["JwtKey"]);

              await _next(httpContext);
            }
            else
            {
              httpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            }
          }
        }
        else
        {
          httpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
        }
      }
    }
  }
}
