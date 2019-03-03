using CJ.Exp.Admin.Models;
using CJ.Exp.DomainInterfaces;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Threading.Tasks;

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
