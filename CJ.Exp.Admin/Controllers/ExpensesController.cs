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

namespace CJ.Exp.Admin.Controllers
{
  [Authorize]
  [Route("[controller]/[action]")]
  public class ExpensesController : ControllerBase
  {
    private const string ExpensesFilterDataKey = "ExpensesFilter";
    private const string ExpensesSuccessMessage = "ExpenseMessageKey";

    private readonly IExpensesService _expensesService;
    private readonly IAuthService _authService;

    public ExpensesController(IExpensesService expensesService, IAuthService authService) : base(ExpensesSuccessMessage)
    {
      _expensesService = expensesService;
      _authService = authService;
    }

    [HttpGet]
    public IActionResult Index()
    {
      return IndexInternal();
    }

    private IActionResult IndexInternal()
    {
      var vm = new ExpensesVM();

      PopulateExpensesFilterFromTempData(vm);
      
      return View("Index", vm);
    }

    [HttpPost]
    public IActionResult Filter(ExpensesVM model)
    {
      var filterSM = Mapper.Map<ExpensesFilterSM>(model.Filter);
      AddTempData(ExpensesFilterDataKey, filterSM);
      model.Filter.IsFiltered = true;

      return View("Index", model);
    }

    [HttpGet]
    public IActionResult GetExpensesData(GridFilterViewModel filter)
    {
      var searchFilter = TempData.Get<ExpensesFilterSM>(ExpensesFilterDataKey);
      if (searchFilter.GridFilter == null)
      {
        searchFilter.GridFilter = new GridRequestSM();
      }

      searchFilter.GridFilter.ItemsPerPage = 3;
      var pageIndex = filter.PageIndex;
      searchFilter.GridFilter.PageNumber = pageIndex >= 0 ? pageIndex : 0;

      var expenses = _expensesService.GetExpenses(searchFilter); // new GridRequestSM {ItemsPerPage = filter.PageSize, PageNumber = filter.PageIndex });
      AddTempData(ExpensesFilterDataKey, searchFilter);

      return new JsonResult(expenses);
    }

    private void PopulateExpensesFilterFromTempData(ExpensesVM model)
    {
      var filter = TempData.Get<ExpensesFilterSM>(ExpensesFilterDataKey);
      if (filter == null)
      {
        model.Filter = GetNewExpensesFilter();
      }
      else
      {
        model.Filter = Mapper.Map<ExpensesFilterVM>(filter);
        model.Filter.IsFiltered = true;

        if (filter.GridFilter != null)
        {
          SetPageOption(model, "CurrentPage", filter.GridFilter.PageNumber.ToString());
        }

        AddTempData(ExpensesFilterDataKey, filter);
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

    //private void PopulateExpensesFilter(ExpensesVM model, ExpensesFilterSM updateFilter = null)
    //{

    //  if (updateFilter != null)
    //  {
    //    // Update the filter - This has come from the Index
    //    model.Filter = Mapper.Map<ExpensesFilterVM>(updateFilter);
    //    model.Filter.IsFiltered = true;
    //    AddTempData(ExpensesFilterDataKey, updateFilter);
    //    //model.ExpenseGrid = _expensesService.GetExpenses(updateFilter, new GridRequestSM { ItemsPerPage = 10, PageNumber = 1});
    //  }
    //  else
    //  {
    //    if (model.Filter == null)
    //    {
    //      model.Filter = new ExpensesFilterVM
    //      {
    //        StartDate = DateTime.Today,
    //        EndDate = DateTime.Today,
    //        IsFiltered = false
    //      };
    //    }
    //    else
    //    {
    //      var filterSM = Mapper.Map<ExpensesFilterSM>(model.Filter);          
    //      AddTempData(ExpensesFilterDataKey, filterSM);
    //      model.Filter.IsFiltered = true;
    //      //model.ExpenseGrid = _expensesService.GetExpenses(filterSM, new GridRequestSM { ItemsPerPage = 10, PageNumber = 1 });
    //    }
    //  }

    //}


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
    public IActionResult BackToIndex()
    {
      return IndexInternal();
    }

    private ExpensesVM GetNewExpensesVM()
    {
      return new ExpensesVM
      {
        //ExpenseGrid = null, //_expensesService.GetExpenses(),
        Filter = new ExpensesFilterVM
        {
          StartDate = DateTime.Today,
          EndDate = DateTime.Today,
          IsFiltered = false
        }
      };
    }

    [HttpPost]
    public async Task<IActionResult> DoAdd(ExpenseVM model)
    {
      if (ModelState.IsValid)
      {
        var exp = Mapper.Map<UpdateExpenseSM>(model);
        var user = await _authService.GetUserByPrincipalAsync(User);
        exp.User = user;
        exp.ExpenseType = new ExpenseTypeSM { Id = model.ExpenseTypeId };
        _expensesService.AddExpense(exp);

        if (_expensesService.BusinessStateValid)
        {
          SetSuccessMessage(SuccessMessageKey, "Expense successfully added");
          return RedirectToAction("Index");
        }
        // Todo: set error message
        //model.SetErrorMessage(result.ResponseCode, "Expense");
        model.SetErrorMessage(_expensesService.BusinessErrors);

      }
      PopulateLists(model);
      return View("Add", model);
    }

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
        var user = await _authService.GetUserByPrincipalAsync(User);
        exp.User = user;
        exp.ExpenseType = new ExpenseTypeSM { Id = model.ExpenseTypeId };
        _expensesService.UpdateExpense(exp);
        if (_expensesService.BusinessStateValid)
        {
          SetSuccessMessage(SuccessMessageKey, "Expense successfully updated");
          return RedirectToAction("Index");
        }

        model.SetErrorMessage(_expensesService.BusinessErrors); // result.ResponseCode, "Expense");

      }
      PopulateLists(model);
      return View("Edit", model);
    }

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
          SetSuccessMessage(SuccessMessageKey, "Expense successfully deleted");
          return RedirectToAction("Index");
        }
        model.SetErrorMessage(_expensesService.BusinessErrors);
      }
      PopulateLists(model);
      return View("Delete", model);
    }

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