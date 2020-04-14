using CJ.Exp.Core;
using CJ.Exp.DomainInterfaces;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace CJ.Exp.Admin
{
    public class SessionInfoMiddleware
  {
    private readonly RequestDelegate _next;

    public SessionInfoMiddleware(RequestDelegate next)
    {
      _next = next;
    }

    public async Task Invoke(HttpContext ctxt, ISessionInfo sessionInfo, IAuthService authService)
    {
      if (ctxt.User.Identity.IsAuthenticated)
      {
        var sessionInfoStr = ctxt.Session.GetString("ExpSession");

        if (sessionInfoStr != null)
        {

          var user = JsonConvert.DeserializeObject<SessionInfo>(sessionInfoStr)?.User;
          sessionInfo.User = user;
        }
        else
        {
          sessionInfo = new SessionInfo
          {
            User = await authService.GetUserByPrincipalAsync(ctxt.User)
          };
          ctxt.Session.SetString("ExpSession", JsonConvert.SerializeObject(sessionInfo));
        }
      }

      await _next.Invoke(ctxt);
    }
  }
}
