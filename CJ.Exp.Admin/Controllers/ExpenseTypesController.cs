using AutoMapper;
using CJ.Exp.Admin.Extensions;
using CJ.Exp.Admin.Models.ExpensesViewModels;
using CJ.Exp.Admin.Models.GridViewModels;
using CJ.Exp.DomainInterfaces;
using CJ.Exp.ServiceModels;
using CJ.Exp.ServiceModels.Expenses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CJ.Exp.Admin.Controllers
{
  [Authorize]
  public class ExpenseTypesController : ControllerBase
  {
    private readonly IExpensesService _expensesService;
    private const string ExpenseTypesFilterDataKey = "ExpenseTypesFilter";

    public ExpenseTypesController(ILoggerFactory loggerFactory, IExpensesService expensesService, ILanguage language) : 
      base(loggerFactory.CreateLogger<ExpenseTypesController>(), language)
    {
      _expensesService = expensesService;
    }

    [HttpGet]
    public IActionResult Index()
    {
      return IndexInternal();
    }

    [HttpPost]
    public IActionResult BackToIndex()
    {
      return IndexInternal();
    }

    private IActionResult IndexInternal()
    {
      var vm = CreateExpenseTypesGridFilterFromTempData();
      return View("Index", vm);
    }

    private ExpenseTypesVM CreateExpenseTypesGridFilterFromTempData()
    {
      var model = new ExpenseTypesVM();

      var filter = TempData.GetGridSearchFilter<ExpensesFilterSM>(ExpenseTypesFilterDataKey) ?? new ExpensesFilterSM();

      if (filter?.GridFilter != null)
      {
        SetPageOption(model, "CurrentPage", filter.GridFilter.PageNumber.ToString());
      }

      TempData.AddGridSearchFilter(ExpenseTypesFilterDataKey, filter);

      return model;

    }
    
    [HttpGet]
    [Route("[controller]/[action]")]
    public IActionResult GetExpenseTypesData(GridFilterViewModel filter)
    {
      var searchFilter = TempData.GetGridSearchFilter<ExpenseTypesFilterSM>(ExpenseTypesFilterDataKey, filter);

      var expenseTypes = _expensesService.GetExpenseTypes(searchFilter);

      return new JsonResult(expenseTypes);
    }

    [HttpPost]
    public IActionResult Add()
    {
      return View("Add", new ExpenseTypeVM());
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
    public IActionResult Edit(string editValue)
    {
      var expSm = _expensesService.GetExpenseTypeById(editValue);

      return View("Edit", Mapper.Map<ExpenseTypeVM>(expSm));
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
    public IActionResult Delete(string editValue)
    {
      var expSm = _expensesService.GetExpenseTypeById(editValue);

      return View("Delete", Mapper.Map<ExpenseTypeVM>(expSm));
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