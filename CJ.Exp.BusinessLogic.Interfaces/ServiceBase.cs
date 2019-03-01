using System;
using System.Collections.Generic;
using CJ.Exp.ServiceModels;

namespace CJ.Exp.BusinessLogic.Interfaces
{


  public class ServiceBase
  {
    
    public List<BusinessErrorSM> BusinessErrors { get; set; }

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
        throw new Exception("Record not found");
      }
    }
  }
}
