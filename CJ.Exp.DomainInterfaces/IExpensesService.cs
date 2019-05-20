using System.Collections.Generic;
using CJ.Exp.ServiceModels.Expenses;
using System.Linq;
using CJ.Exp.Core;

namespace CJ.Exp.DomainInterfaces
{
  public interface IExpensesService
  {
    List<ExpenseSM> GetExpenses();

    ExpenseSM AddExpense(ExpenseSM expense);

    ExpenseSM UpdateExpense(ExpenseSM expense);

    bool DeleteExpense(ExpenseSM expense);

    List<ExpenseTypeSM> GetExpenseTypes();

    ServiceResponse<ExpenseTypeSM> AddExpenseType(ExpenseTypeSM expenseType);

    ExpenseTypeSM UpdateExpenseType(ExpenseTypeSM expenseType);

    bool DeleteExpenseType(ExpenseTypeSM expenseType);
  }
}
