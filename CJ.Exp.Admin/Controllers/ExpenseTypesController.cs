using AutoMapper;
using CJ.Exp.Admin.Models.ExpensesViewModels;
using CJ.Exp.DomainInterfaces;
using CJ.Exp.ServiceModels.Expenses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using CJ.Exp.Core;

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
    public IActionResult DoAdd(ExpenseTypeVM model)
    {
      if (ModelState.IsValid)
      {
        var result = _expensesService.AddExpenseType(Mapper.Map<ExpenseTypeSM>(model));
        if (result.ResponseCode == ServiceResponseCode.Success)
        {
          return RedirectToAction("Index");
        }
        model.SetErrorMessage(result.ResponseCode, "Expense Type");
      }
      return View("Add", model);
    }

    [HttpPost]
    public IActionResult Cancel()
    {
      return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult Edit()
    {
      return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult DoEdit()
    {
      return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult Edit(string expenseTypeId)
    {
      var expSm = _expensesService.GetExpenseTypes().SingleOrDefault(x => x.Id == expenseTypeId);

      return View(Mapper.Map<ExpenseTypeVM>(expSm));
    }

    [HttpPost]
    public IActionResult DoEdit(ExpenseTypeVM model)
    {
      if (ModelState.IsValid)
      {
        var result = _expensesService.UpdateExpenseType(Mapper.Map<UpdateExpenseTypeSM>(model));
        return RedirectToAction("Index");
      }
      return View(model);      
    }

    [HttpPost]
    public IActionResult Delete(string expenseTypeId)
    {
      var expSm = _expensesService.GetExpenseTypes().SingleOrDefault(x => x.Id == expenseTypeId);

      return View(Mapper.Map<ExpenseTypeVM>(expSm));
    }

    [HttpGet]
    public IActionResult Delete()
    {
      return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult DoDelete(ExpenseTypeVM model)
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