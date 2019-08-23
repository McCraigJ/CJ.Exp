using System;
using System.Collections.Generic;
using System.Text;

namespace CJ.Exp.BusinessLogic
{
  public class CjExpInvalidOperationException : InvalidOperationException
  {
    public CjExpInvalidOperationException(string message):base(message)
    {
      
    }
  }

  public class CjExpDeveloperException : Exception
  {
    public CjExpDeveloperException(string message) : base(message)
    {

    }
  }
}
