using System.Collections.Generic;
using CJ.Exp.ServiceModels;

namespace CJ.Exp.Admin.Models
{
  public class ViewModelBase
  {
    public List<string> ErrorMessages { get; set; }

    public Dictionary<string, string> Options { get; set; }

    public ViewModelBase()
    {
      ErrorMessages = new List<string>();
    }
    //public void SetErrorMessage(ServiceResponseCode responseCode, string subject, string name = null)
    //{
    //  switch (responseCode)
    //  {
    //    case ServiceResponseCode.DataInUse:
    //      ErrorMessage = $"{subject} could not be deleted as it is being used";
    //      break;

    //    case ServiceResponseCode.DataAlreadyExists:
    //      ErrorMessage = $"A {subject} called {name} already exists";
    //      break;

    //    case ServiceResponseCode.DataNotFound:
    //      ErrorMessage = $"Data could not be found";
    //      break;

    //    case ServiceResponseCode.UnknownError:
    //      ErrorMessage = "An unknown error has occurred";
    //      break;

    //  }
    //}

    public void SetErrorMessage(List<BusinessErrorSM> businessErrors)
    {
      foreach (var err in businessErrors)
      {
        //Todo: Use text cache system
        ErrorMessages.Add(err.ErrorMessage);
      }
    }
  }
}
