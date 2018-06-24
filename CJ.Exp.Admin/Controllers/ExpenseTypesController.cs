using CJ.Exp.Admin.Models.ExpensesViewModels;
using CJ.Exp.BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CJ.Exp.Admin.Controllers
{
  [Authorize]
  [Route("[controller]/[action]")]
  public class ExpenseTypesController : Controller
  {

    private readonly IExpensesService _expensesService;

    public ExpenseTypesController(IExpensesService expensesService)
    {
      _expensesService = expensesService;
    }

    [HttpGet]
    public IActionResult Index()
    {
      var vm = new ExpenseTypesVM
      {
        ExpenseTypes = _expensesService.GetExpenseTypes().ToList()        
      };
      return View(vm);
    }
  }
}