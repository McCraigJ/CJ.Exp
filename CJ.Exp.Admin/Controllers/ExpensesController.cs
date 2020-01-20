﻿using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CJ.Exp.Admin.Extensions;
using CJ.Exp.Admin.Models.ExpensesViewModels;
using CJ.Exp.Admin.Models.GridViewModels;
using CJ.Exp.DomainInterfaces;
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
      TempData.AddGridSearchFilter(ExpensesFilterDataKey, Mapper.Map<ExpensesFilterSM>(model));
      model.IsFiltered = true;

      return View("Index", model);
    }

    [HttpGet]
    [Route("[controller]/[action]")]
    public async Task<IActionResult> GetExpensesDataAsync(GridFilterViewModel filter)
    {
      var searchFilter = TempData.GetGridSearchFilter<ExpensesFilterSM>(ExpensesFilterDataKey, filter);

      var expenses = await _expensesService.GetExpensesAsync(searchFilter);

      return new JsonResult(expenses);
    }


    private ExpensesFilterVM CreateExpensesFilterFromTempData()
    {

      var filter = TempData.GetGridSearchFilter<ExpensesFilterSM>(ExpensesFilterDataKey);
      if (filter == null)
      {
        return GetNewExpensesFilter();
      }

      var model = Mapper.Map<ExpensesFilterVM>(filter);
      model.IsFiltered = true;

      if (filter.GridFilter != null)
      {
        SetPageOption(model, "CurrentPage", filter.GridFilter.PageNumber.ToString());
      }

      TempData.AddGridSearchFilter(ExpensesFilterDataKey, filter);

      return model;

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
    public async Task<IActionResult> Add()
    {
      var vm = new ExpenseVM
      {
        ExpenseDate = DateTime.Today
      };
      await PopulateListsAsync(vm);
      return View("Add", vm);
    }

    [HttpPost]
    public async Task<IActionResult> DoAdd(ExpenseVM model)
    {
      if (ModelState.IsValid)
      {
        var exp = Mapper.Map<UpdateExpenseSM>(model);
        exp.ExpenseType = new ExpenseTypeSM { Id = model.ExpenseTypeId };
        await _expensesService.AddExpenseAsync(exp);

        if (_expensesService.BusinessStateValid)
        {
          SetControllerMessage(ControllerMessageType.Success, "Added");
          return RedirectToAction("Index");
        }
        model.SetErrorMessage(_expensesService.BusinessErrors, Language);
      }
      await PopulateListsAsync(model);
      return View("Add", model);
    }

    #endregion

    #region Edit

    [HttpPost]
    public async Task<IActionResult> Edit(string editValue, int currentPage)
    {
      var expSm = await _expensesService.GetExpenseByIdAsync(editValue);

      var vm = Mapper.Map<ExpenseVM>(expSm);

      await PopulateListsAsync(vm);
      return View("Edit", vm);
    }

    [HttpPost]
    public async Task<IActionResult> DoEdit(ExpenseVM model)
    {
      if (ModelState.IsValid)
      {
        var exp = Mapper.Map<UpdateExpenseSM>(model);
        exp.ExpenseType = new ExpenseTypeSM { Id = model.ExpenseTypeId };
        await _expensesService.UpdateExpenseAsync(exp);

        if (_expensesService.BusinessStateValid)
        {
          SetControllerMessage(ControllerMessageType.Success, "Updated");
          return RedirectToAction("Index");
        }

        model.SetErrorMessage(_expensesService.BusinessErrors, Language);

      }
      await PopulateListsAsync(model);
      return View("Edit", model);
    }

    #endregion

    #region Delete

    [HttpPost]
    public async Task<IActionResult> Delete(string editValue)
    {
      var expSm = await _expensesService.GetExpenseByIdAsync(editValue);

      var vm = Mapper.Map<ExpenseVM>(expSm);

      await PopulateListsAsync(vm);
      return View("Delete", vm);
    }

    [HttpPost]
    public async Task<IActionResult> DoDelete(ExpenseVM model)
    {
      if (ModelState.IsValid)
      {
        var exp = Mapper.Map<ExpenseSM>(model);

        await _expensesService.DeleteExpenseAsync(exp);
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
      await PopulateListsAsync(model);
      return View("Delete", model);
    }

    #endregion

    private async Task PopulateListsAsync(ExpenseVM model)
    {
      var list = await _expensesService.GetExpenseTypesAsync();
        
      model.ExpenseTypes = list.Select(x =>
        new SelectListItem
        {
          Text = x.ExpenseType,
          Value = x.Id.ToString()
        }).ToList();
    }

  }
}