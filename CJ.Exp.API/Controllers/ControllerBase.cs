using AutoMapper;
using CJ.Exp.ApiModels;
using CJ.Exp.DomainInterfaces;
using CJ.Exp.ServiceModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CJ.Exp.API.Controllers
{
  public class ControllerBase : Controller
  {
    protected ILanguage _language { get; }
    public ControllerBase(ILanguage language)
    {
      _language = language;
    }
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
      var response = new ApiResponseAM
      {
        Success = true,
        BusinessErrors = null
      };
      return Ok(response);
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
      if (responseModel.BusinessErrors != null)
      {
        foreach (var err in responseModel.BusinessErrors)
        {
          err.ErrorMessage = _language.GetText(err.ErrorMessage);
        }
        
      }
      return Ok(responseModel);
    }
  }
}
