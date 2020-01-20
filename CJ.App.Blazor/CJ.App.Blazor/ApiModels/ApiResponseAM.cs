using System.Collections.Generic;

namespace CJ.App.Blazor.ApiModels
{
  public class ApiResponseAM<T> where T : ApiResponseModelBase
  {
    public bool Success { get; set; }
    public List<BusinessErrorAM> BusinessErrors { get; set; }
    public T Data { get; set; }

  }
}
