using System;
using System.Collections.Generic;
using System.Linq;
using CJ.Exp.ServiceModels;
using CJ.Exp.ServiceModels.Expenses;

namespace CJ.Exp.Data.Interfaces
{
  public interface IExpensesData : IBaseData
  {
    ExpenseSM AddExpense(ExpenseSM expense);

    ExpenseTypeSM AddExpenseType(ExpenseTypeSM expenseType);

    bool DeleteExpense(ExpenseSM expense);

    bool DeleteExpenseType(ExpenseTypeSM expenseType);

    ExpenseSummarySM GetExpenses(ExpensesFilterSM filter, GridRequestSM gridRequest);

    ExpenseSM GetExpenseById(string id);

    List<ExpenseTypeSM> GetExpenseTypes();

    ExpenseTypeSM GetExpenseTypeById(string id);

    ExpenseTypeSM GetExpenseTypeByName(string expenseTypeName);

    ExpenseSM UpdateExpense(ExpenseSM expense);

    ExpenseTypeSM UpdateExpenseType(ExpenseTypeSM expenseType);

    bool UpdateExpenseWithUpdatedExpenseType(ExpenseTypeSM expenseType);

  }
}
