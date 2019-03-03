using CJ.Exp.Data.Interfaces;
using CJ.Exp.DomainInterfaces;
using CJ.Exp.ServiceModels.Expenses;
using System.Linq;

namespace CJ.Exp.BusinessLogic.Expenses
{
  public class ExpensesService : IExpensesService
  {
    private IExpensesData _data;

    public ExpensesService(IExpensesData data)
    {
      _data = data;
    }

    public ExpenseSM AddExpense(ExpenseSM expense)
    {
      return _data.AddExpense(expense);      
    }

    public ExpenseTypeSM AddExpenseType(ExpenseTypeSM expenseType)
    {
      return _data.AddExpenseType(expenseType);
    }

    public bool DeleteExpense(ExpenseSM expense)
    {
      return _data.DeleteExpense(expense);
    }

    public bool DeleteExpenseType(ExpenseTypeSM expenseType)
    {
      return _data.DeleteExpenseType(expenseType);
    }

    public IQueryable<ExpenseSM> GetExpenses()
    {
      return _data.GetExpenses();
    }

    public IQueryable<ExpenseTypeSM> GetExpenseTypes()
    {
      return _data.GetExpenseTypes();
    }

    public ExpenseSM UpdateExpense(ExpenseSM expense)
    {
      return _data.UpdateExpense(expense);
    }

    public ExpenseTypeSM UpdateExpenseType(ExpenseTypeSM expenseType)
    {
      return _data.UpdateExpenseType(expenseType);

    }
  }
}
