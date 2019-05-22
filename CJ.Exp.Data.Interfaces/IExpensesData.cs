using System;
using System.Collections.Generic;
using System.Linq;
using CJ.Exp.ServiceModels.Expenses;

namespace CJ.Exp.Data.Interfaces
{
  public interface IExpensesData : IBaseData
  {
    UpdateExpenseSM AddExpense(UpdateExpenseSM expense);

    ExpenseTypeSM AddExpenseType(ExpenseTypeSM expenseType);

    bool DeleteExpense(ExpenseSM expense);

    bool DeleteExpenseType(ExpenseTypeSM expenseType);

    List<ExpenseSM> GetExpenses();

    List<ExpenseTypeSM> GetExpenseTypes();

    ExpenseTypeSM GetExpenseTypeById(string id);

    ExpenseTypeSM GetExpenseTypeByName(string expenseTypeName);

    ExpenseSM UpdateExpense(ExpenseSM expense);

    ExpenseTypeSM UpdateExpenseType(ExpenseTypeSM expenseType);

    bool UpdateExpenseWithUpdatedExpenseType(ExpenseTypeSM expenseType);

  }
}
