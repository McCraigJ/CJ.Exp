using CJ.Exp.ServiceModels;

namespace CJ.Exp.Core
{
  public enum ServiceResponseCode
  {
    Success = 0,
    DataAlreadyExists = 1,
    DataNotFound = 2,
    UnknownError = 3
  }

  public class ServiceResponse<TModel> where TModel : ServiceModelBase
  {
    public ServiceResponseCode ResponseCode { get; set; }
    public TModel Model { get; set; }

    public ServiceResponse(TModel model, ServiceResponseCode responseCode)
    {
      Model = model;
      ResponseCode = responseCode;
    }
    
  }
}
