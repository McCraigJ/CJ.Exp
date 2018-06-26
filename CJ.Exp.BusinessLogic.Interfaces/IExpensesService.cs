using CJ.Exp.ServiceModels.Expenses;
using System;
using System.Linq;

namespace CJ.Exp.BusinessLogic.Interfaces
{
  public interface IExpensesService
  {
    IQueryable<ExpenseSM> GetExpenses();

    ExpenseSM AddExpense(ExpenseSM expense);

    ExpenseSM UpdateExpense(ExpenseSM expense);

    bool DeleteExpense(ExpenseSM expense);

    IQueryable<ExpenseTypeSM> GetExpenseTypes();

    ExpenseTypeSM AddExpenseType(ExpenseTypeSM expenseType);

    ExpenseTypeSM UpdateExpenseType(ExpenseTypeSM expenseType);

    bool DeleteExpenseType(ExpenseTypeSM expenseType);
  }
}
