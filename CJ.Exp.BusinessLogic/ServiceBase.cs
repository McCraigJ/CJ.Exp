using System;
using System.Collections.Generic;
using System.Text;

namespace CJ.Exp.BusinessLogic
{
  public class ServiceBase
  {
    public void AssertObjectNotNull(object obj)
    {
      if (obj == null)
      {
        throw new Exception("Record not found");
      }
    }
  }
}
