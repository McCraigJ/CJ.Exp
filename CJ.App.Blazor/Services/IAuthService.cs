using System.Threading.Tasks;
using CJ.Exp.ApiModels;

namespace CJ.App.Blazor.Services
{
  public interface IAuthService
  {
    Task<ApiResponseAM<LoginResponseAM>> Login(LoginAM loginModel);

    Task Logout();
  }
}