using CJ.Exp.ServiceModels;
using CJ.Exp.ServiceModels.Expenses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CJ.Exp.Data.Interfaces
{
  public interface IExpensesData : IBaseData
  {
    Task<ExpenseSM> AddExpenseAsync(ExpenseSM expense);

    Task<ExpenseTypeSM> AddExpenseTypeAsync(ExpenseTypeSM expenseType);

    Task<bool> DeleteExpenseAsync(ExpenseSM expense);

    Task<bool> DeleteExpenseTypeAsync(ExpenseTypeSM expenseType);

    Task<GridResultSM<ExpenseSM>> GetExpensesAsync(ExpensesFilterSM filter);

    Task<ExpenseSM> GetExpenseByIdAsync(string id);

    Task<GridResultSM<ExpenseTypeSM>> GetExpenseTypesAsync(ExpenseTypesFilterSM filter);

    Task<List<ExpenseTypeSM>> GetExpenseTypesAsync();

    Task<ExpenseTypeSM> GetExpenseTypeByIdAsync(string id);

    Task<ExpenseTypeSM> GetExpenseTypeByNameAsync(string expenseTypeName);

    Task<ExpenseSM> UpdateExpenseAsync(ExpenseSM expense);

    Task<ExpenseTypeSM> UpdateExpenseTypeAsync(ExpenseTypeSM expenseType);

    Task<bool> UpdateExpenseWithUpdatedExpenseTypeAsync(ExpenseTypeSM expenseType);

  }
}
