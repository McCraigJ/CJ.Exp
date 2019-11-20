using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CJ.Exp.ServiceModels;

namespace CJ.Exp.API.ApiModels
{
  public class ApiResponseAM
  {
    public bool Success { get; set; }
    public List<BusinessErrorSM> BusinessErrors { get; set; }
    public object Data { get; set; }

  }
}
