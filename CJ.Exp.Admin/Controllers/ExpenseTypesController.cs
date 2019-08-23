using AutoMapper;
using CJ.Exp.Admin.Models.ExpensesViewModels;
using CJ.Exp.DomainInterfaces;
using CJ.Exp.ServiceModels.Expenses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.Extensions.Logging;

namespace CJ.Exp.Admin.Controllers
{
  [Authorize]
  [Route("[controller]/[action]")]
  public class ExpenseTypesController : ControllerBase
  {
    private readonly IExpensesService _expensesService;

    public ExpenseTypesController(ILoggerFactory loggerFactory, IExpensesService expensesService, ILanguage language) : 
      base(loggerFactory.CreateLogger<ExpenseTypesController>(), language)
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
        _expensesService.AddExpenseType(Mapper.Map<ExpenseTypeSM>(model));
        if (_expensesService.BusinessStateValid)
        {
          SetControllerMessage(ControllerMessageType.Success,"Added");
          return RedirectToAction("Index");
        }
        model.SetErrorMessage(_expensesService.BusinessErrors, Language);
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
        _expensesService.UpdateExpenseType(Mapper.Map<UpdateExpenseTypeSM>(model));
        if (_expensesService.BusinessStateValid)
        {
          SetControllerMessage(ControllerMessageType.Success, "Updated");
          return RedirectToAction("Index");
        }

        model.SetErrorMessage(_expensesService.BusinessErrors, Language);
      }
      return View("Edit", model);
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
        _expensesService.DeleteExpenseType(Mapper.Map<ExpenseTypeSM>(model));
        if (_expensesService.BusinessStateValid)
        {
          SetControllerMessage(ControllerMessageType.Success, "Deleted");
          return RedirectToAction("Index");
        }
        model.SetErrorMessage(_expensesService.BusinessErrors, Language);
        
      }
      return View("Delete", model);
    }
  }
}