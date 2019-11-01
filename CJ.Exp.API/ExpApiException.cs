using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CJ.Exp.API
{

  public class ExpApiException : Exception
  {
    public ExpApiException(string message) : base (message)
    {
    }
  }
}
