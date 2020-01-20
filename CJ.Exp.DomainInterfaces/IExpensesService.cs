using System.Collections.Generic;
using System.Threading.Tasks;
using CJ.Exp.ServiceModels.Expenses;
using CJ.Exp.ServiceModels;

namespace CJ.Exp.DomainInterfaces
{
  public interface IExpensesService : IBusinessErrors
  {
    Task<GridResultSM<ExpenseSM>> GetExpensesAsync(ExpensesFilterSM filter);

    Task<ExpenseSM> GetExpenseByIdAsync(string id);

    Task AddExpenseAsync(UpdateExpenseSM expense);

    Task UpdateExpenseAsync(UpdateExpenseSM expense);

    Task DeleteExpenseAsync(ExpenseSM expense);

    Task<GridResultSM<ExpenseTypeSM>> GetExpenseTypesAsync(ExpenseTypesFilterSM filter);

    Task<List<ExpenseTypeSM>> GetExpenseTypesAsync();

    Task<ExpenseTypeSM> GetExpenseTypeByIdAsync(string id);

    Task AddExpenseTypeAsync(ExpenseTypeSM expenseType);

    Task UpdateExpenseTypeAsync(UpdateExpenseTypeSM expenseType);

    Task DeleteExpenseTypeAsync(ExpenseTypeSM expenseType);
  }
}
