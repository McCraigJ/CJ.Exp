using System.Threading.Tasks;
using CJ.Exp.DomainInterfaces;
using CJ.Exp.ServiceModels.Users;

namespace CJ.Exp.BusinessLogic
{
  public class CjExpService : ServiceBase
  {
    private readonly IAuthService _authService;
    private readonly IServiceInfo _serviceInfo;

    public CjExpService(IAuthService authService, IServiceInfo serviceInfo)
    {
      _authService = authService;
      _serviceInfo = serviceInfo;
    }

    public async Task<UserSM> GetCurrentUser()
    {
      if (_serviceInfo.CurrentClaimsPrincipal == null)
      {
        throw new CjExpInvalidOperationException("Cannot get the logged in User");
      }

      var user = await _authService.GetUserByPrincipalAsync(_serviceInfo.CurrentClaimsPrincipal);

      if (user == null)
      {
        throw new CjExpInvalidOperationException("Cannot get the logged in User");
      }

      return user;

    }

  }
}
