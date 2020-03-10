using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System.Linq;

namespace CJ.Exp.API.Extensions
{
  public static class HttpContextExtensions
  {
    public static string GetAuthorisationToken(this HttpContext httpContext)
    {
      string token = null;

      if (httpContext.Request.Headers.TryGetValue("authorization", out StringValues authHeaderValues))
      {
        token = authHeaderValues.FirstOrDefault()?.Replace("bearer ", "").Replace("Bearer ", "");
      }

      return token;
    }
  }
}
