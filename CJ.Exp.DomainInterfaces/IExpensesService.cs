using System.Collections.Generic;
using CJ.Exp.ServiceModels.Expenses;
using System.Linq;
using CJ.Exp.Core;
using CJ.Exp.ServiceModels;

namespace CJ.Exp.DomainInterfaces
{
  public interface IExpensesService
  {
    GridResultSM<ExpenseSM> GetExpenses(ExpensesFilterSM filter, GridRequestSM gridRequest);

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
