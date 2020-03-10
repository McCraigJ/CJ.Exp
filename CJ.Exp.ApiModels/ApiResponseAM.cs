using System.Collections.Generic;

namespace CJ.Exp.ApiModels
{
  public enum ApiBusinessErrorCodes
  {
    DataNotFound,
    DataAlreadyExists,
    CouldNotUpdate,
    Generic
  }

  public class BusinessErrorAM
  {
    public ApiBusinessErrorCodes ErrorCode { get; private set; }
    public string ErrorMessage { get; private set; }

    public BusinessErrorAM(ApiBusinessErrorCodes errorCode, string errorMessage)
    {
      ErrorCode = errorCode;
      ErrorMessage = errorMessage;
    }
  }

  public class ApiResponseAM<T> : ApiResponseAM
  {
    public T Data { get; set; }

  }

  public class ApiResponseAM 
  {
    public bool Success { get; set; }
    public List<BusinessErrorAM> BusinessErrors { get; set; }
  }
}
