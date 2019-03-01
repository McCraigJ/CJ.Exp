using System;
using System.Linq;
using CJ.Exp.ServiceModels.Expenses;

namespace CJ.Exp.Data.Interfaces
{
  public interface IExpensesData
  {
    ExpenseSM AddExpense(ExpenseSM expense);

    ExpenseTypeSM AddExpenseType(ExpenseTypeSM expenseType);

    bool DeleteExpense(ExpenseSM expense);

    bool DeleteExpenseType(ExpenseTypeSM expenseType);

    IQueryable<ExpenseSM> GetExpenses();

    IQueryable<ExpenseTypeSM> GetExpenseTypes();

    ExpenseSM UpdateExpense(ExpenseSM expense);

    ExpenseTypeSM UpdateExpenseType(ExpenseTypeSM expenseType);
  }
}
