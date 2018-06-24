using CJ.Exp.ServiceModels.Expenses;
using System;
using System.Linq;

namespace CJ.Exp.BusinessLogic.Interfaces
{
  public interface IExpensesService
  {
    IQueryable<ExpenseSM> GetExpenses();

    ExpenseSM AddExpense();

    ExpenseSM UpdateExpense();

    bool DeleteExpense();

    IQueryable<ExpenseTypeSM> GetExpenseTypes();

    ExpenseTypeSM AddExpenseType();

    ExpenseTypeSM UpdateExpenseType();

    bool DeleteExpenseType();
  }
}
