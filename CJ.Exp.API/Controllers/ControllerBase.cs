using System;
using System.Collections.Generic;
using System.Net;
using CJ.Exp.API.ApiModels;
using CJ.Exp.ServiceModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CJ.Exp.API.Controllers
{
  public class ControllerBase : Controller
  {
    public IActionResult SuccessResponse(object data)
    {
      var response = new ApiResponseAM
      {
        Success = true,
        BusinessErrors = null,
        Data = data
      };

      return CreateResponse(response);
    }

    public IActionResult SuccessResponse()
    {
      return SuccessResponse(null);
    }

    public IActionResult BusinessErrorResponse(string errorMessage)
    {
      var businessErrors = new List<BusinessErrorSM>();
      businessErrors.Add(new BusinessErrorSM(BusinessErrorCodes.Generic, errorMessage));

      var response = new ApiResponseAM
      {
        Success = false,
        BusinessErrors = businessErrors,
        Data = null
      };

      return CreateResponse(response);
    }

    public IActionResult BusinessErrorResponse(List<BusinessErrorSM> errors)
    {
      var response = new ApiResponseAM
      {
        Success = false,
        BusinessErrors = errors,
        Data = null
      };

      return CreateResponse(response);
    }

    private IActionResult CreateResponse(ApiResponseAM responseModel)
    {
      return Ok(responseModel);
    }
  }
}
