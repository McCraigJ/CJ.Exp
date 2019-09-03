using System;
using System.Collections.Generic;
using CJ.Exp.Admin.Extensions;
using CJ.Exp.Admin.Models;
using CJ.Exp.BusinessLogic;
using CJ.Exp.DomainInterfaces;
using CJ.Exp.ServiceModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace CJ.Exp.Admin.Controllers
{
  public enum ControllerMessageType
  {
    Success = 1,
    Error = 2
  }

  public class ControllerBase : Controller
  {
    public ILogger Logger { get; }
    public ILanguage Language { get; }
    
    public ControllerBase(ILogger logger, ILanguage language)
    {
      Logger = logger;
      Language = language;
    }

    [Serializable]
    public class ControllerMessage
    {
      public string Message { get; set; }
      public DateTime MessageDateTime { get; set; }
      public ControllerMessageType ControllerMessageType { get; set; }
    }

    protected void MergeBusinessErrors(List<BusinessErrorSM> businessErrors)
    {      
      foreach (var err in businessErrors)
      {
        ModelState.AddModelError("Id", $"BusinessError.{Language.GetText(err.ErrorMessage)}");
      }
    }

    //protected void AddTempData<T>(string key, T data) where T : class
    //{
    //  TempData.Put<T>(key, data);
    //}

    //protected T GetTempData<T>(string key, bool isRequired = true) where T : class
    //{
    //  T data = TempData.Get<T>(key);

    //  if (isRequired && data == null)
    //  {
    //    throw new CjExpInvalidOperationException("Cannot find data");
    //  }

    //  return data;
    //}

    protected void SetControllerMessage(ControllerMessageType messageType, string message)
    {
      var msg = new ControllerMessage
      {
        Message = GetControllerText(message),
        ControllerMessageType = messageType,
        MessageDateTime = DateTime.Now
      };

      TempData.AddTempData(controllerMessageKey, msg);
    }

    protected ControllerMessage GetControllerMessage()
    {
      var msg = TempData.GetTempData<ControllerMessage>(controllerMessageKey, false);

      if (msg != null)
      {
        var currentTime = DateTime.Now;
        if (currentTime - msg.MessageDateTime < TimeSpan.FromSeconds(30))
        {
          return msg;
        }
      }

      return null;
    }

    private string controllerMessageKey => $"{ControllerName}ControllerMsg";

    public override void OnActionExecuting(ActionExecutingContext context)
    {
      ViewData["controllerMessage"] = GetControllerMessage();
      
      base.OnActionExecuting(context);
    }

    public void SetPageOption<T>(T viewModel, string key, string value) where T : ViewModelBase
    {
      if (viewModel.Options == null)
      {
        viewModel.Options = new Dictionary<string, string>();
      }

      if (viewModel.Options.ContainsKey(key))
      {
        viewModel.Options[key] = value;
      }
      else
      {
        viewModel.Options.Add(key, value);
      }
    }

    protected string GetControllerText(string action)
    {
      return Language.GetText($"{ControllerName}.{action}");
    }


    private string ControllerName => this.ControllerContext.RouteData.Values["controller"].ToString();

  }
}
