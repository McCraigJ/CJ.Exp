using CJ.Exp.Admin.Models.ExpensesViewModels;
using CJ.Exp.BusinessLogic.Interfaces;
using CJ.Exp.ServiceModels.Expenses;
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

    [HttpGet]
    public IActionResult Add()
    {
      return View(new ExpenseTypeVM());
    }

    [HttpPost]
    public IActionResult Add(ExpenseTypeVM model)
    {
      if (ModelState.IsValid)
      {
        _expensesService.AddExpenseType(AutoMapper.Mapper.Map<ExpenseTypeSM>(model));
        return RedirectToAction("Index");
      }
      return View(model);
    }
  }
}