using System;
using System.Collections.Generic;
using System.Text;

namespace CJ.Exp.ServiceModels.AuthTokens
{
  public class AuthTokenSM
  {
    public string Id { get; set; }
    public string Token { get; set; }
    public DateTime Issued { get; set; }
    public DateTime Expiry { get; set; }
  }
}
