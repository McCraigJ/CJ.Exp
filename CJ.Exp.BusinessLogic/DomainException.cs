using System;
using System.Collections.Generic;
using System.Text;

namespace CJ.Exp.BusinessLogic
{
  public class DomainException : Exception
  {
    public DomainException(string message):base(message)
    {
      
    }
  }
}
