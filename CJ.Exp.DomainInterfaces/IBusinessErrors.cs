using CJ.Exp.ServiceModels;
using System.Collections.Generic;

namespace CJ.Exp.DomainInterfaces
{
  public interface IBusinessErrors
  {
    List<BusinessErrorSM> BusinessErrors { get; }
  }
}
