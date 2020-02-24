using AutoMapper;
using CJ.Exp.ApiModels;
using CJ.Exp.DomainInterfaces;
using CJ.Exp.ServiceModels;
using CJ.Exp.ServiceModels.Expenses;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CJ.Exp.API.Controllers
{
  [Route("api/[controller]/[action]")]
  public class ExpensesController : ControllerBase
  {
    private readonly IExpensesService _expensesService;

    public ExpensesController(IExpensesService expensesService)
    {
      _expensesService = expensesService;
    }

    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> GetExpenses(ExpensesFilterAM model)
    {
      var filter = new ExpensesFilterSM
      {
        StartDate = model.StartDate,
        EndDate = model.EndDate,
        GridFilter = new GridRequestSM
        {
          ItemsPerPage = model.ItemsPerPage,
          PageNumber = model.PageNumber
        }
      };
      var expenses = await _expensesService.GetExpensesAsync(filter);

      return Ok(new
      {
        expenses = expenses.GridRows
      });

    }

    [HttpPost]
    public async Task<IActionResult> Add(AddExpenseAM model)
    {
      var expense = Mapper.Map<UpdateExpenseSM>(model);
      expense.ExpenseType = new ExpenseTypeSM { Id = model.ExpenseTypeId };
      await _expensesService.AddExpenseAsync(expense);

      if (_expensesService.BusinessStateValid)
      {
        return SuccessResponse();
      }

      return BusinessErrorResponse(_expensesService.BusinessErrors);

    }
  }
}