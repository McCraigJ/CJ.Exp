using CJ.Exp.ServiceModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace CJ.Exp.Core.BusinessLogic
{
  public interface IBusinessErrors
  {
    List<BusinessErrorSM> BusinessErrors { get; set; }
  }
}
