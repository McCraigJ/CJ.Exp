using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CJ.Exp.Admin.Extensions;
using CJ.Exp.Admin.Models;
using CJ.Exp.BusinessLogic;
using CJ.Exp.ServiceModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;

namespace CJ.Exp.Admin.Controllers
{
  public class ControllerBase : Controller
  {
    protected string SuccessMessageKey { get; set; }

    public ControllerBase(string successMessageKey)
    {
      SuccessMessageKey = successMessageKey;
    }

    [Serializable]
    private class TempDataMessage
    {
      public string Message { get; set; }
      public DateTime MessageDateTime { get; set; }
    }

    protected void MergeBusinessErrors(List<BusinessErrorSM> businessErrors)
    {      
      foreach (var err in businessErrors)
      {
        ModelState.AddModelError("Id", err.ErrorMessage);
      }
    }

    protected void RemoveTempData(string key)
    {
      TempData.Remove(key);
    }

    protected void AddTempData<T>(string key, T data) where T : class
    {
      TempData.Put<T>(key, data);
    }

    protected T GetTempData<T>(string key, bool isRequired = true) where T : class
    {
      T data = TempData.Get<T>(key);

      if (isRequired && data == null)
      {
        throw new DomainException("Cannot find data");
      }

      return data;
    }

    protected void SetSuccessMessage(string key, string message)
    {
      var msg = new TempDataMessage
      {
        Message = message,
        MessageDateTime = DateTime.Now
      };

      AddTempData(key, msg);      
    }

    protected string GetSuccessMessage(string key)
    {
      var msg = GetTempData<TempDataMessage>(key, false);

      if (msg != null)
      {
        var currentTime = DateTime.Now;
        if (currentTime - msg.MessageDateTime < TimeSpan.FromSeconds(30))
        {
          return msg.Message;
        }
      }

      return null;
    }

    public override void OnActionExecuting(ActionExecutingContext context)
    {
      ViewData["Message"] = GetSuccessMessage(SuccessMessageKey);
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

  }
}
