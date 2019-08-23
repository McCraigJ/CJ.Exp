using System.Collections.Generic;
using System.Threading.Tasks;
using CJ.Exp.ServiceModels.Expenses;
using CJ.Exp.ServiceModels;

namespace CJ.Exp.DomainInterfaces
{
  public interface IExpensesService : IBusinessErrors
  {
    GridResultSM<ExpenseSM> GetExpenses(ExpensesFilterSM filter);

    ExpenseSM GetExpenseById(string id);

    Task AddExpense(UpdateExpenseSM expense);

    Task UpdateExpense(UpdateExpenseSM expense);

    void DeleteExpense(ExpenseSM expense);

    List<ExpenseTypeSM> GetExpenseTypes();

    void AddExpenseType(ExpenseTypeSM expenseType);

    void UpdateExpenseType(UpdateExpenseTypeSM expenseType);

    void DeleteExpenseType(ExpenseTypeSM expenseType);
  }
}
