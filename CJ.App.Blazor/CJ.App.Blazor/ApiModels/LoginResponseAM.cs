using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CJ.App.Blazor.ApiModels
{
  public class LoginResponseAM : ApiResponseModelBase
  {
    public string Id { get; set; }
    public string Email { get; set; }
    public string Token { get; set; }
    public string RefreshToken { get; set; }
    public string Error { get; set; }
  }
}
