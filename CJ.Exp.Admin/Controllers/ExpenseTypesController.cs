using AutoMapper;
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
        _expensesService.AddExpenseType(Mapper.Map<ExpenseTypeSM>(model));
        return RedirectToAction("Index");
      }
      return View(model);
    }

    [HttpGet]
    public IActionResult Edit(int id)
    {
      var expSm = _expensesService.GetExpenseTypes().SingleOrDefault(x => x.Id == id);

      return View(Mapper.Map<ExpenseTypeVM>(expSm));
    }

    [HttpPost]
    public IActionResult Edit(ExpenseTypeVM model)
    {
      if (ModelState.IsValid)
      {
        var updatedModel = _expensesService.UpdateExpenseType(Mapper.Map<ExpenseTypeSM>(model));
        return RedirectToAction("Index");
      }
      return View(model);      
    }

    [HttpGet]
    public IActionResult Delete(int id)
    {
      var expSm = _expensesService.GetExpenseTypes().SingleOrDefault(x => x.Id == id);

      return View(Mapper.Map<ExpenseTypeVM>(expSm));
    }

    [HttpPost]
    public IActionResult Delete(ExpenseTypeVM model)
    {
      if (ModelState.IsValid)
      {
        var deleted = _expensesService.DeleteExpenseType(Mapper.Map<ExpenseTypeSM>(model));
        if (deleted)
        {
          return RedirectToAction("Index");
        }
        model.ErrorMessage = "Expense Type could not be deleted as it is being used";
      }
      return View(model);
    }
  }
}