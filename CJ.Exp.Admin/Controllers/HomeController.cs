using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CJ.Exp.Admin.Models;
using CJ.Exp.BusinessLogic.Interfaces;
using CJ.Exp.ServiceModels;
using Microsoft.AspNetCore.Authorization;

namespace CJ.Exp.Admin.Controllers
{
  public class HomeController : Controller
  {
    private readonly IAuthService _authService;

    public HomeController(IAuthService authService)
    {
      _authService = authService;
    }
    public IActionResult Index()
    {
      return View();
    }

    public IActionResult About()
    {
      ViewData["Message"] = "Your application description page.";

      return View();
    }

    public IActionResult Contact()
    {
      ViewData["Message"] = "Your contact page.";

      return View();
    }
    
    public async Task<IActionResult> SeedData()
    {
      await _authService.SeedData();
      return RedirectToAction("Index");
    }

    public IActionResult Error()
    {
      return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
  }
}
