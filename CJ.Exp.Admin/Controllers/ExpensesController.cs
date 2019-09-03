using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CJ.Exp.Admin.Extensions;
using CJ.Exp.Admin.Models.ExpensesViewModels;
using CJ.Exp.Admin.Models.GridViewModels;
using CJ.Exp.DomainInterfaces;
using CJ.Exp.ServiceModels;
using CJ.Exp.ServiceModels.Expenses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;

namespace CJ.Exp.Admin.Controllers
{
  [Authorize]
  public class ExpensesController : ControllerBase
  {
    private const string ExpensesFilterDataKey = "ExpensesFilter";

    private readonly IExpensesService _expensesService;

    public ExpensesController(ILoggerFactory loggerFactory, IExpensesService expensesService, ILanguage language) : 
      base(loggerFactory.CreateLogger<ExpensesController>(), language)
    {
      _expensesService = expensesService;
    }

    #region Index
    
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
      var vm = CreateExpensesFilterFromTempData();

      return View("Index", vm);
    }

    [HttpPost]
    public IActionResult Filter(ExpensesFilterVM model)
    {
      var filterSM = Mapper.Map<ExpensesFilterSM>(model);
      AddTempData(ExpensesFilterDataKey, filterSM);
      model.IsFiltered = true;

      return View("Index", model);
    }

    [HttpGet]
    [Route("[controller]/[action]")]
    public IActionResult GetExpensesData(GridFilterViewModel filter)
    {
      var searchFilter = TempData.Get<ExpensesFilterSM>(ExpensesFilterDataKey);
      if (searchFilter.GridFilter == null)
      {
        searchFilter.GridFilter = new GridRequestSM();
      }

      searchFilter.GridFilter.ItemsPerPage = 20;
      var pageIndex = filter.PageIndex;
      searchFilter.GridFilter.PageNumber = pageIndex >= 0 ? pageIndex : 0;

      var expenses = _expensesService.GetExpenses(searchFilter);
      AddTempData(ExpensesFilterDataKey, searchFilter);

      return new JsonResult(expenses);
    }

    private ExpensesFilterVM CreateExpensesFilterFromTempData()
    {

      var filter = TempData.Get<ExpensesFilterSM>(ExpensesFilterDataKey);
      if (filter == null)
      {
        return GetNewExpensesFilter();
      }
      else
      {
        var model = Mapper.Map<ExpensesFilterVM>(filter);
        model.IsFiltered = true;

        if (filter.GridFilter != null)
        {
          SetPageOption(model, "CurrentPage", filter.GridFilter.PageNumber.ToString());
        }

        AddTempData(ExpensesFilterDataKey, filter);

        return GetNewExpensesFilter();
      }
    }

    private ExpensesFilterVM GetNewExpensesFilter()
    {
      return new ExpensesFilterVM
      {
        StartDate = DateTime.Today,
        EndDate = DateTime.Today,
        IsFiltered = false
      };
    }

    #endregion

    #region Add

    [HttpPost]
    public IActionResult Add()
    {
      var vm = new ExpenseVM
      {
        ExpenseDate = DateTime.Today
      };
      PopulateLists(vm);
      return View("Add", vm);
    }

    [HttpPost]
    public async Task<IActionResult> DoAdd(ExpenseVM model)
    {
      if (ModelState.IsValid)
      {
        var exp = Mapper.Map<UpdateExpenseSM>(model);
        exp.ExpenseType = new ExpenseTypeSM { Id = model.ExpenseTypeId };
        await _expensesService.AddExpense(exp);

        if (_expensesService.BusinessStateValid)
        {
          SetControllerMessage(ControllerMessageType.Success, "Added");
          return RedirectToAction("Index");
        }
        model.SetErrorMessage(_expensesService.BusinessErrors, Language);
      }
      PopulateLists(model);
      return View("Add", model);
    }

    #endregion

    #region Edit

    [HttpPost]
    public IActionResult Edit(string editValue, int currentPage)
    {
      var expSm = _expensesService.GetExpenseById(editValue);

      var vm = Mapper.Map<ExpenseVM>(expSm);

      PopulateLists(vm);
      return View("Edit", vm);
    }

    [HttpPost]
    public async Task<IActionResult> DoEdit(ExpenseVM model)
    {
      if (ModelState.IsValid)
      {
        var exp = Mapper.Map<UpdateExpenseSM>(model);
        exp.ExpenseType = new ExpenseTypeSM { Id = model.ExpenseTypeId };
        await _expensesService.UpdateExpense(exp);

        if (_expensesService.BusinessStateValid)
        {
          SetControllerMessage(ControllerMessageType.Success, "Updated");
          return RedirectToAction("Index");
        }

        model.SetErrorMessage(_expensesService.BusinessErrors, Language);

      }
      PopulateLists(model);
      return View("Edit", model);
    }

    #endregion

    #region Delete

    [HttpPost]
    public IActionResult Delete(string editValue)
    {
      var expSm = _expensesService.GetExpenseById(editValue);

      var vm = Mapper.Map<ExpenseVM>(expSm);

      PopulateLists(vm);
      return View("Delete", vm);
    }

    [HttpPost]
    public IActionResult DoDelete(ExpenseVM model)
    {
      if (ModelState.IsValid)
      {
        var exp = Mapper.Map<ExpenseSM>(model);

        _expensesService.DeleteExpense(exp);
        if (_expensesService.BusinessStateValid)
        {
          SetControllerMessage(ControllerMessageType.Success, "Deleted");
        }
        else
        {
          SetControllerMessage(ControllerMessageType.Error, "DeleteError");
        }

        return RedirectToAction("Index");
      }
      PopulateLists(model);
      return View("Delete", model);
    }

    #endregion

    private void PopulateLists(ExpenseVM model)
    {
      var list = _expensesService.GetExpenseTypes().Select(x =>
         new SelectListItem
         {
           Text = x.ExpenseType,
           Value = x.Id.ToString()
         }).ToList();
      model.ExpenseTypes = list;
    }

  }
}