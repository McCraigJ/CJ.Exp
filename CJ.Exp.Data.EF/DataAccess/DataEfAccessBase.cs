using CJ.Exp.ServiceModels;
using System;
using System.Collections.Generic;

namespace CJ.Exp.Data.EF.DataAccess
{
  public class DataEfAccessBase
  {
    protected readonly ExpDbContext _data;
    public List<BusinessErrorSM> BusinessErrors { get; set; }

    public DataEfAccessBase(ExpDbContext data)
    {
      _data = data;
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
