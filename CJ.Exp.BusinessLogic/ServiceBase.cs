using System.Collections.Generic;
using System.Linq;
using CJ.Exp.ServiceModels;

namespace CJ.Exp.BusinessLogic
{
  public class ServiceBase
  {
    public List<BusinessErrorSM> BusinessErrors { get; set; }

    public bool BusinessStateValid => !BusinessErrors.Any();

    public ServiceBase()
    {
      BusinessErrors = new List<BusinessErrorSM>();
    }

    public void AddBusinessError(BusinessErrorCodes errorCode, string errorMessage)
    {
      BusinessErrors.Add(new BusinessErrorSM(errorCode, errorMessage));
    }

    public void AssertObjectNotNull(object obj)
    {
      if (obj == null)
      {
        throw new CjExpInvalidOperationException("Record not found");
      }
    }

  }
}
