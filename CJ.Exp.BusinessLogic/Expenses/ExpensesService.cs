using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using CJ.Exp.Data.Interfaces;
using CJ.Exp.DomainInterfaces;
using CJ.Exp.ServiceModels;
using CJ.Exp.ServiceModels.Expenses;
using CJ.Exp.ServiceModels.Users;

namespace CJ.Exp.BusinessLogic.Expenses
{
  public class ExpensesService : CjExpService, IExpensesService
  {
    private readonly IExpensesData _data;

    public ExpensesService(IExpensesData data, IAuthService authService, IServiceInfo serviceInfo) : base(authService, serviceInfo)
    {
      _data = data;
    }

    public ExpenseSM GetExpenseById(string id)
    {
      return _data.GetExpenseById(id);
    }

    public async Task AddExpense(UpdateExpenseSM expense)
    {
      var expenseType = _data.GetExpenseTypeById(expense.ExpenseType.Id);
      if (expenseType == null)
      {
        AddBusinessError(BusinessErrorCodes.DataNotFound, "ExpenseTypeNotFound");
      }

      if (BusinessStateValid)
      {
        expense.User = await GetCurrentUser();

        expense.ExpenseType = expenseType;
        _data.AddExpense(expense);
      }
      
    }

    public async Task UpdateExpense(UpdateExpenseSM expense)
    {
      var exp = GetExpenseById(expense.Id);
      if (exp == null)
      {
        AddBusinessError(BusinessErrorCodes.DataNotFound, "ExpenseNotFound");
      }

      if (BusinessStateValid)
      {
        _data.StartTransaction();

        exp.User = await GetCurrentUser();
        
        if (string.IsNullOrEmpty(expense.NewExpenseType))
        {
          expense.ExpenseType = _data.GetExpenseTypeById(expense.ExpenseType.Id);
        }
        else
        {
          var expenseType = AddExpenseTypeInternal(new ExpenseTypeSM { ExpenseType = expense.NewExpenseType });
          expense.ExpenseType = expenseType;
        }

        if (BusinessStateValid)
        {
          _data.UpdateExpense(expense);

          _data.CommitTransaction();
        } 
      }
    }

    

    private ExpenseTypeSM AddExpenseTypeInternal(ExpenseTypeSM expenseType)
    {
      if (_data.GetExpenseTypeByName(expenseType.ExpenseType) != null)
      {
        AddBusinessError(BusinessErrorCodes.DataAlreadyExists, "ExpenseTypeAlreadyExists");
      }

      if (BusinessStateValid)
      {
        return _data.AddExpenseType(expenseType);
      }

      return null;
    }

    public void AddExpenseType(ExpenseTypeSM expenseType)
    {
      AddExpenseTypeInternal(expenseType);
    }

    public void DeleteExpense(ExpenseSM expense)
    {
      if (_data.GetExpenseById(expense.Id) == null)
      {
        AddBusinessError(BusinessErrorCodes.DataNotFound, "ExpenseNotFound");
      }
      else
      {
        _data.DeleteExpense(expense);
      }
        
    }

    public void DeleteExpenseType(ExpenseTypeSM expenseType)
    {
      if (_data.GetExpenseTypeById(expenseType.Id) == null)
      {
        AddBusinessError(BusinessErrorCodes.DataNotFound, "ExpenseTypeNotFound");
      }
      else
      {
        _data.DeleteExpenseType(expenseType);
      }
    }

    public GridResultSM<ExpenseSM> GetExpenses(ExpensesFilterSM filter)
    {
      if (filter?.GridFilter == null)
      {
        throw new CjExpInvalidOperationException("No Grid Request Data has been supplied");
      }
      return _data.GetExpenses(filter);
    }

    public List<ExpenseTypeSM> GetExpenseTypes()
    {
      return _data.GetExpenseTypes();
    }

    public void UpdateExpenseType(UpdateExpenseTypeSM expenseType)
    {
      _data.StartTransaction();
      _data.UpdateExpenseType(expenseType);

      if (expenseType.UpdateExpensesWithUpdatedType)
      {
        _data.UpdateExpenseWithUpdatedExpenseType(expenseType);
      }
      _data.CommitTransaction();

    }
  }
}
