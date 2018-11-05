using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CJ.Exp.ServiceModels;
using Microsoft.AspNetCore.Mvc;

namespace CJ.Exp.Admin.Controllers
{
  public class ControllerBase : Controller
  {
    protected void MergeBusinessErrors(List<BusinessErrorSM> businessErrors)
    {      
      foreach (var err in businessErrors)
      {
        ModelState.AddModelError("Id", err.ErrorMessage);
      }
    }
  }
}
