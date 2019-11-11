using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using CJ.Exp.API.Extensions;
using CJ.Exp.DomainInterfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;

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

    public async Task InvokeAsync(HttpContext httpContext, IAuthTokenService authTokenService, IAuthService authService)
    {
      var path = httpContext.Request.Path.Value.ToLowerInvariant();

      if (path.Contains("/users/login") || path.Contains("/users/register"))
      {
        await _next(httpContext);
      }
      else
      {
        var token = httpContext.GetAuthorisationToken();
        if (token != null)
        {
          if (authTokenService.HasAuthToken(token))
          {
            await _next(httpContext);
          }
          else
          {
            httpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
          }
        }
      }
        
    }
  }
}
