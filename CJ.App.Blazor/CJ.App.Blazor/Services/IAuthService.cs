using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CJ.App.Blazor.ApiModels;

namespace CJ.App.Blazor.Services
{
  public interface IAuthService
  {
    Task<ApiResponseAM<LoginResponseAM>> Login(LoginAM loginModel);

    Task Logout();
  }
}
