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

    public ExpenseSM GetExpenseById(string id)
    {
      return _data.GetExpenseById(id);
    }

    public async Task AddExpense(UpdateExpenseSM expense)
    {

      if (string.IsNullOrEmpty(expense.NewExpenseType))
      {
        var expenseType = expense.ExpenseType == null ? null : _data.GetExpenseTypeById(expense.ExpenseType.Id);
        if (expenseType == null)
        {
          AddBusinessError(BusinessErrorCodes.DataNotFound, "ExpenseTypeNotFound");
        }
        else
        {
          expense.ExpenseType = _data.GetExpenseTypeById(expense.ExpenseType.Id);
        }
      }
      else
      {
        expense.ExpenseType = AddExpenseTypeInternal(new ExpenseTypeSM { ExpenseType = expense.NewExpenseType }); ;
      }

      if (BusinessStateValid)
      {
        expense.User = _sessionInfo.User;

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

        exp.User = _sessionInfo.User;

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

    public GridResultSM<ExpenseTypeSM> GetExpenseTypes(ExpenseTypesFilterSM filter)
    {
      return _data.GetExpenseTypes(filter);
    }

    public List<ExpenseTypeSM> GetExpenseTypes()
    {
      return _data.GetExpenseTypes();
    }

    public ExpenseTypeSM GetExpenseTypeById(string id)
    {
      var expenseType = _data.GetExpenseTypeById(id);
      if (expenseType == null)
      {
        AddBusinessError(BusinessErrorCodes.DataNotFound, "ExpenseTypeNotFound");
      }

      return expenseType;
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
