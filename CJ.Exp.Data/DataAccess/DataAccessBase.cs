using System;
using System.Collections.Generic;
using System.Text;
using CJ.Exp.ServiceModels;

namespace CJ.Exp.Data.DataAccess
{
  public class DataAccessBase
  {
    protected readonly ExpDbContext _data;
    public List<BusinessErrorSM> BusinessErrors { get; set; }

    public DataAccessBase(ExpDbContext data)
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
