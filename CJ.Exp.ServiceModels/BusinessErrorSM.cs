namespace CJ.Exp.ServiceModels
{
  public enum BusinessErrorCodes
  {
    DataNotFound,
    DataAlreadyExists,
    CouldNotUpdate,
    Generic
  }
  public class BusinessErrorSM
  {
    public BusinessErrorCodes ErrorCode { get; private set; }
    public string ErrorMessage { get; private set; }

    public BusinessErrorSM(BusinessErrorCodes errorCode, string errorMessage)
    {
      ErrorCode = errorCode;
      ErrorMessage = errorMessage;
    }
  }
}
