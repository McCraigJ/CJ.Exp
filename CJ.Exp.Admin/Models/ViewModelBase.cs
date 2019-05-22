using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CJ.Exp.Core;

namespace CJ.Exp.Admin.Models
{
  public class ViewModelBase
  {
    public string ErrorMessage { get; set; }

    public void SetErrorMessage(ServiceResponseCode responseCode, string subject, string name = null)
    {
      switch (responseCode)
      {
        case ServiceResponseCode.DataInUse:
          ErrorMessage = $"{subject} could not be deleted as it is being used";
          break;

        case ServiceResponseCode.DataAlreadyExists:
          ErrorMessage = $"A {subject} called {name} already exists";
          break;

        case ServiceResponseCode.DataNotFound:
          ErrorMessage = $"Data could not be found";
          break;

        case ServiceResponseCode.UnknownError:
          ErrorMessage = "An unknown error has occurred";
          break;

      }
    }
  }
}
