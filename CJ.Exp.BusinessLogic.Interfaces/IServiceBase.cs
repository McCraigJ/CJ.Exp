using System;
using System.Collections.Generic;
using System.Text;
using CJ.Exp.ServiceModels;

namespace CJ.Exp.BusinessLogic.Interfaces
{
  public interface IServiceBase
  {
    List<BusinessErrorSM> BusinessErrors { get; set; }
  }
}
