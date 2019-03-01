//using System;
//using System.Collections.Generic;
//using System.Text;
//using CJ.Exp.Data;
//using CJ.Exp.ServiceModels;

//namespace CJ.Exp.BusinessLogic
//{

  
//  public class ServiceBase
//  {
//    protected readonly ExpDbContext _data;
//    public List<BusinessErrorSM> BusinessErrors { get; set; }

//    public ServiceBase(ExpDbContext data)
//    {
//      _data = data;
//      BusinessErrors = new List<BusinessErrorSM>();
//    }


//    public void AddBusinessError(BusinessErrorCodes errorCode, string errorMessage)
//    {
//      BusinessErrors.Add(new BusinessErrorSM(errorCode, errorMessage));
//    }

//    public void AssertObjectNotNull(object obj)
//    {
//      if (obj == null)
//      {
//        throw new Exception("Record not found");
//      }
//    }
//  }
//}
