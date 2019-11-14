using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CJ.Exp.API.ApiModels
{
  public class RefreshTokenAM
  {
    public string CurrentToken { get; set; }
    public string RefreshToken { get; set; }
  }
}
