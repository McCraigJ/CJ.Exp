using CJ.Exp.Data.Interfaces;
using CJ.Exp.DomainInterfaces;
using CJ.Exp.ServiceModels;
using CJ.Exp.ServiceModels.Expenses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CJ.Exp.BusinessLogic.Expenses
{
  public class ExpensesService : ServiceBase, IExpensesService
  {
    private readonly IExpensesData _data;
    private readonly ISessionInfo _sessionInfo;

    public ExpensesService(IExpensesData data, ISessionInfo sessionInfo)
    {
      _data = data;
      _sessionInfo = sessionInfo;
    }

    public async Task<ExpenseSM> GetExpenseByIdAsync(string id)
    {
      return await _data.GetExpenseByIdAsync(id);
    }

    public async Task AddExpenseAsync(UpdateExpenseSM expense)
    {

      if (string.IsNullOrEmpty(expense.NewExpenseType))
      {
        var expenseType = expense.ExpenseType == null ? null : await _data.GetExpenseTypeByIdAsync(expense.ExpenseType.Id);
        if (expenseType == null)
        {
          AddBusinessError(BusinessErrorCodes.DataNotFound, "ExpenseTypeNotFound");
        }
        else
        {
          expense.ExpenseType = await _data.GetExpenseTypeByIdAsync(expense.ExpenseType.Id);
        }
      }
      else
      {
        expense.ExpenseType = await AddExpenseTypeInternalAsync(new ExpenseTypeSM { ExpenseType = expense.NewExpenseType }); ;
      }

      if (BusinessStateValid)
      {
        expense.User = _sessionInfo.User;

        await _data.AddExpenseAsync(expense);
      }
      
    }

    public async Task UpdateExpenseAsync(UpdateExpenseSM expense)
    {
      var exp = await GetExpenseByIdAsync(expense.Id);
      if (exp == null)
      {
        AddBusinessError(BusinessErrorCodes.DataNotFound, "ExpenseNotFound");
      }

      if (BusinessStateValid)
      {
        _data.StartTransaction();

        exp.User = _sessionInfo.User;

        if (string.IsNullOrEmpty(expense.NewExpenseType))
        {
          expense.ExpenseType = await _data.GetExpenseTypeByIdAsync(expense.ExpenseType.Id);
        }
        else
        {
          var expenseType = await AddExpenseTypeInternalAsync(new ExpenseTypeSM { ExpenseType = expense.NewExpenseType });
          expense.ExpenseType = expenseType;
        }

        if (BusinessStateValid)
        {
          await _data.UpdateExpenseAsync(expense);

          _data.CommitTransaction();
        } 
      }
    }

    

    private async Task<ExpenseTypeSM> AddExpenseTypeInternalAsync(ExpenseTypeSM expenseType)
    {
      if (await _data.GetExpenseTypeByNameAsync(expenseType.ExpenseType) != null)
      {
        AddBusinessError(BusinessErrorCodes.DataAlreadyExists, "ExpenseTypeAlreadyExists");
      }

      if (BusinessStateValid)
      {
        return await _data.AddExpenseTypeAsync(expenseType);
      }

      return null;
    }

    public async Task AddExpenseTypeAsync(ExpenseTypeSM expenseType)
    {
      await AddExpenseTypeInternalAsync(expenseType);
    }

    public async Task DeleteExpenseAsync(ExpenseSM expense)
    {
      if (await _data.GetExpenseByIdAsync(expense.Id) == null)
      {
        AddBusinessError(BusinessErrorCodes.DataNotFound, "ExpenseNotFound");
      }
      else
      {
        await _data.DeleteExpenseAsync(expense);
      }
        
    }

    public async Task DeleteExpenseTypeAsync(ExpenseTypeSM expenseType)
    {
      if (await _data.GetExpenseTypeByIdAsync(expenseType.Id) == null)
      {
        AddBusinessError(BusinessErrorCodes.DataNotFound, "ExpenseTypeNotFound");
      }
      else
      {
        await _data.DeleteExpenseTypeAsync(expenseType);
      }
    }

    public async Task<GridResultSM<ExpenseSM>> GetExpensesAsync(ExpensesFilterSM filter)
    {
      if (filter?.GridFilter == null)
      {
        throw new CjExpInvalidOperationException("No Grid Request Data has been supplied");
      }
      return await _data.GetExpensesAsync(filter);
    }

    public async Task<GridResultSM<ExpenseTypeSM>> GetExpenseTypesAsync(ExpenseTypesFilterSM filter)
    {
      return await _data.GetExpenseTypesAsync(filter);
    }

    public async Task<List<ExpenseTypeSM>> GetExpenseTypesAsync()
    {
      return await _data.GetExpenseTypesAsync();
    }

    public async Task<ExpenseTypeSM> GetExpenseTypeByIdAsync(string id)
    {
      var expenseType = await _data.GetExpenseTypeByIdAsync(id);
      if (expenseType == null)
      {
        AddBusinessError(BusinessErrorCodes.DataNotFound, "ExpenseTypeNotFound");
      }

      return expenseType;
    }

    public async Task UpdateExpenseTypeAsync(UpdateExpenseTypeSM expenseType)
    {
      _data.StartTransaction();
      await _data.UpdateExpenseTypeAsync(expenseType);

      if (expenseType.UpdateExpensesWithUpdatedType)
      {
        await _data.UpdateExpenseWithUpdatedExpenseTypeAsync(expenseType);
      }
      _data.CommitTransaction();

    }
  }
}
