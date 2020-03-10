using AutoMapper;
using CJ.Exp.ApiModels;
using CJ.Exp.ServiceModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CJ.Exp.API.Controllers
{
  public class ControllerBase : Controller
  {
    public IActionResult SuccessResponse<T>(T data)
    {
      var response = new ApiResponseAM<T>
      {
        Success = true,
        BusinessErrors = null,
        Data = data
      };

      return Ok(response);
    }

    public IActionResult SuccessResponse()
    {
      return Ok(null);
    }

    public IActionResult BusinessErrorResponse(string errorMessage)
    {
      var businessErrors = new List<BusinessErrorAM>();
      businessErrors.Add(new BusinessErrorAM(ApiBusinessErrorCodes.Generic, errorMessage));

      var response = new ApiResponseAM
      {
        Success = false,
        BusinessErrors = businessErrors
      };

      return CreateResponse(response);
    }

    public IActionResult BusinessErrorResponse(List<BusinessErrorSM> errors)
    {
      var apiErrors = Mapper.Map<List<BusinessErrorAM>>(errors);

      var response = new ApiResponseAM
      {
        Success = false,
        BusinessErrors = apiErrors
      };

      return CreateResponse(response);
    }

    private IActionResult CreateResponse(ApiResponseAM responseModel)
    {
      return Ok(responseModel);
    }
  }
}
