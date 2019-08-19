﻿using AutoMapper;
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
using CJ.Exp.Admin.Models.GridViewModels;
using CJ.Exp.ServiceModels;

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

      var vm = new ExpensesVM();

      var filter = TempData.Get<ExpensesFilterSM>(ExpensesFilterDataKey);
      PopulateFilteredExpenses(vm, filter);


      return View(vm);
    }

    [HttpPost]
    public IActionResult Filter(ExpensesVM model)
    {
      PopulateFilteredExpenses(model);

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

      var expenses = _expensesService.GetExpenses(searchFilter, null); // new GridRequestSM {ItemsPerPage = filter.PageSize, PageNumber = filter.PageIndex });
      AddTempData(ExpensesFilterDataKey, searchFilter);

      return new JsonResult(expenses);
    }

    private void PopulateFilteredExpenses(ExpensesVM model, ExpensesFilterSM updateFilter = null)
    {

      if (updateFilter != null)
      {
        // Update the filter - This has come from the Index
        model.Filter = Mapper.Map<ExpensesFilterVM>(updateFilter);
        model.Filter.IsFiltered = true;
        AddTempData(ExpensesFilterDataKey, updateFilter);
        //model.ExpenseGrid = _expensesService.GetExpenses(updateFilter, new GridRequestSM { ItemsPerPage = 10, PageNumber = 1});
      }
      else
      {
        if (model.Filter == null)
        {
          model.Filter = new ExpensesFilterVM
          {
            StartDate = DateTime.Today,
            EndDate = DateTime.Today,
            IsFiltered = false
          };
        }
        else
        {
          var filterSM = Mapper.Map<ExpensesFilterSM>(model.Filter);          
          AddTempData(ExpensesFilterDataKey, filterSM);
          model.Filter.IsFiltered = true;
          //model.ExpenseGrid = _expensesService.GetExpenses(filterSM, new GridRequestSM { ItemsPerPage = 10, PageNumber = 1 });
        }
      }

    }


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
      var filter = TempData.Get<ExpensesFilterSM>(ExpensesFilterDataKey);
      //var filter = TempDataHelper.Get<ExpensesFilterSM>(TempData, "ExpensesFilter");  // (ExpensesFilterSM)TempData["ExpenseFilter"];      
      if (filter != null)
      {
        var model = new ExpensesVM
        {
          //ExpenseGrid = _expensesService.GetExpenses(filter, new GridRequestSM { ItemsPerPage = 10, PageNumber = 1 }),
          Filter = Mapper.Map<ExpensesFilterVM>(filter)
        };
        if (model.Filter != null)
        {
          model.Filter.IsFiltered = true;
        }

        return View("Index", model);
      }

      return View("Index", GetNewExpensesVM());

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

        SetSuccessMessage(SuccessMessageKey, "Expense successfully added");
        return RedirectToAction("Index");
      }
      PopulateLists(model);
      return View(model);
    }

    [HttpPost]
    public IActionResult Edit(string editValue, int currentPage)
    {
      var expSm = _expensesService.GetExpenseById(editValue);

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