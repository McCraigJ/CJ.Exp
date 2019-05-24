using AutoMapper;
using CJ.Exp.Admin.Models.ExpensesViewModels;
using CJ.Exp.DomainInterfaces;
using CJ.Exp.ServiceModels.Expenses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CJ.Exp.Admin.Extensions;

namespace CJ.Exp.Admin.Controllers
{
  [Authorize]
  [Route("[controller]/[action]")]
  public class ExpensesController : Controller
  {

    private readonly IExpensesService _expensesService;
    private readonly IAuthService _authService;

    public ExpensesController(IExpensesService expensesService, IAuthService authService)
    {
      _expensesService = expensesService;
      _authService = authService;
    }

    [HttpGet]
    public IActionResult Index()
    {
      var vm = new ExpensesVM
      {
        Expenses = null, //_expensesService.GetExpenses(),
        Filter = new ExpensesFilterVM
        {
          StartDate = DateTime.Today,
          EndDate = DateTime.Today,
          IsFiltered = false
        }
      };
      return View(vm);
    }

    [HttpPost]
    public IActionResult Filter(ExpensesVM model)
    {
      var filter = Mapper.Map<ExpenseFilterSM>(model.Filter);
      model.Filter.IsFiltered = true;

      TempDataHelper.Put<ExpenseFilterSM>(TempData, "ExpensesFilter", filter);
      model.Expenses = _expensesService.GetExpenses(filter);
      
      return View("Index", model);
    }

    [HttpGet]
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
      var filter = TempDataHelper.Get<ExpenseFilterSM>(TempData, "ExpensesFilter");  // (ExpenseFilterSM)TempData["ExpenseFilter"];      
      if (filter != null)
      {
        var model = new ExpensesVM
        {
          Expenses = _expensesService.GetExpenses(filter),
          Filter = Mapper.Map<ExpensesFilterVM>(filter)          
        };
        return View("Index", model);
      }

      return View("Index", GetNewExpensesVM());

    }

    private ExpensesVM GetNewExpensesVM()
    {
      return new ExpensesVM
      {
        Expenses = null, //_expensesService.GetExpenses(),
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
        return RedirectToAction("Index");
      }
      PopulateLists(model);
      return View(model);
    }

    [HttpPost]
    public IActionResult Edit(string id)
    {
      var expSm = _expensesService.GetExpenseById(id);

      var vm = Mapper.Map<ExpenseVM>(expSm);

      PopulateLists(vm);
      return View(vm);
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
        return RedirectToAction("Index");
      }
      PopulateLists(model);
      return View(model);
    }

    [HttpPost]
    public IActionResult Delete(string id)
    {
      var expSm = _expensesService.GetExpenseById(id);

      var vm = Mapper.Map<ExpenseVM>(expSm);

      PopulateLists(vm);
      return View(vm);
    }

    [HttpPost]
    public IActionResult DoDelete(ExpenseVM model)
    {
      if (ModelState.IsValid)
      {
        var exp = Mapper.Map<ExpenseSM>(model);

        var deleted = _expensesService.DeleteExpense(exp);
        if (deleted)
        {
          return RedirectToAction("Index");
        }
        else
        {
          model.ErrorMessage = "Expense could not be deleted";
        }
      }
      PopulateLists(model);
      return View(model);
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