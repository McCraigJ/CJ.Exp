using AutoMapper;
using CJ.Exp.Admin.Models.ExpensesViewModels;
using CJ.Exp.DomainInterfaces;
using CJ.Exp.ServiceModels.Expenses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Linq;
using System.Threading.Tasks;

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
        Expenses = _expensesService.GetExpenses().ToList()
      };
      return View(vm);
    }

    [HttpGet]
    public IActionResult Add()
    {
      var vm = new ExpenseVM
      {
        ExpenseDate = DateTime.Today
      };
      PopulateLists(vm);
      return View(vm);
    }

    [HttpPost]
    public async Task<IActionResult> Add(ExpenseVM model)
    {
      if (ModelState.IsValid)
      {
        var exp = Mapper.Map<ExpenseSM>(model);
        var user = await _authService.GetUserByPrincipalAsync(User);
        exp.User = user;
        exp.ExpenseType = new ExpenseTypeSM { Id = model.ExpenseTypeId };
        _expensesService.AddExpense(exp);
        return RedirectToAction("Index");
      }
      PopulateLists(model);
      return View(model);
    }

    [HttpGet]
    public IActionResult Edit(int id)
    {
      var expSm = _expensesService.GetExpenses().SingleOrDefault(x => x.Id == id);

      var vm = Mapper.Map<ExpenseVM>(expSm);

      PopulateLists(vm);
      return View(vm);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(ExpenseVM model)
    {
      if (ModelState.IsValid)
      {
        var exp = Mapper.Map<ExpenseSM>(model);
        var user = await _authService.GetUserByPrincipalAsync(User);
        exp.User = user;
        exp.ExpenseType = new ExpenseTypeSM { Id = model.ExpenseTypeId };
        _expensesService.UpdateExpense(exp);
        return RedirectToAction("Index");
      }
      PopulateLists(model);
      return View(model);
    }

    [HttpGet]
    public IActionResult Delete(int id)
    {
      var expSm = _expensesService.GetExpenses().SingleOrDefault(x => x.Id == id);

      var vm = Mapper.Map<ExpenseVM>(expSm);

      PopulateLists(vm);
      return View(vm);
    }

    [HttpPost]
    public IActionResult Delete(ExpenseVM model)
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