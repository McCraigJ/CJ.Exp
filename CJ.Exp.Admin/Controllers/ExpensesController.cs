using CJ.Exp.Admin.Models.ExpensesViewModels;
using CJ.Exp.BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace CJ.Exp.Admin.Controllers
{
  [Authorize]
  [Route("[controller]/[action]")]
  public class ExpensesController : Controller
  {

    private readonly IExpensesService _expensesService;

    public ExpensesController(IExpensesService expensesService)
    {
      _expensesService = expensesService;
    }

    [HttpGet]
    public IActionResult Index()
    {
      var vm = new ExpensesVM
      {
        Expenses = _expensesService.GetExpenses().ToList()
      };
      return View(vm);
    }
  }
}