using System.Collections.Generic;
using CJ.Exp.ServiceModels.Expenses;
using System.Linq;
using CJ.Exp.Core;

namespace CJ.Exp.DomainInterfaces
{
  public interface IExpensesService
  {
    List<ExpenseSM> GetExpenses(ExpenseFilterSM filter);

    ExpenseSM GetExpenseById(string id);

    ServiceResponse<UpdateExpenseSM> AddExpense(UpdateExpenseSM expense);

    ExpenseSM UpdateExpense(UpdateExpenseSM expense);

    bool DeleteExpense(ExpenseSM expense);

    List<ExpenseTypeSM> GetExpenseTypes();

    ServiceResponse<ExpenseTypeSM> AddExpenseType(ExpenseTypeSM expenseType);

    ServiceResponse<UpdateExpenseTypeSM> UpdateExpenseType(UpdateExpenseTypeSM expenseType);

    bool DeleteExpenseType(ExpenseTypeSM expenseType);
  }
}
