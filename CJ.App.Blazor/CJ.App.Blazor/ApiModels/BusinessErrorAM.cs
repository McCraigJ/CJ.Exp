using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CJ.App.Blazor.ApiModels
{
  public enum BusinessErrorCodes
  {
    DataNotFound,
    DataAlreadyExists,
    CouldNotUpdate,
    Generic
  }
  public class BusinessErrorAM
  {
    public BusinessErrorCodes ErrorCode { get; set; }
    public string ErrorMessage { get; set; }
  }
}
