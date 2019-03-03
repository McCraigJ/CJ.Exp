﻿using AutoMapper.QueryableExtensions;
using CJ.Exp.ServiceModels.Expenses;
using System.Collections.Generic;

namespace CJ.Exp.Data.EF.DataAccess
{
  using AutoMapper;
  using global::CJ.Exp.Data.EF.DataModels;
  using global::CJ.Exp.Data.Interfaces;
  using System.Linq;

  namespace CJ.Exp.BusinessLogic
  {

    public class ExpensesData : DataAccessBase, IExpensesData
    {
      public ExpensesData(ExpDbContext data) : base(data) { }

      public ExpenseSM AddExpense(ExpenseSM expense)
      {
        var exp = Mapper.Map<ExpenseDM>(expense);
        exp.ExpenseType = _data.ExpenseTypes.SingleOrDefault(x => x.Id == expense.ExpenseType.Id);
        _data.Expenses.Add(exp);
        _data.SaveChanges();
        expense.Id = exp.Id;
        return expense;
      }

      public ExpenseTypeSM AddExpenseType(ExpenseTypeSM expenseType)
      {
        var exp = Mapper.Map<ExpenseTypeDM>(expenseType);
        _data.ExpenseTypes.Add(exp);
        _data.SaveChanges();
        expenseType.Id = exp.Id;
        return expenseType;
      }

      public bool DeleteExpense(ExpenseSM expense)
      {

        var exp = _data.Expenses.SingleOrDefault(x => x.Id == expense.Id);
        if (exp == null)
        {
          return false;
        }
        _data.Remove(exp);
        _data.SaveChanges();

        return true;

      }

      public bool DeleteExpenseType(ExpenseTypeSM expenseType)
      {
        var exp = _data.ExpenseTypes.SingleOrDefault(x => x.Id == expenseType.Id);
        if (exp == null)
        {
          return false;
        }
        _data.Remove(exp);
        _data.SaveChanges();

        return true;

      }

      public IQueryable<ExpenseSM> GetExpenses()
      {
        return (from e in _data.Expenses select e).ProjectTo<ExpenseSM>();
      }

      public IQueryable<ExpenseTypeSM> GetExpenseTypes()
      {
        return (from t in _data.ExpenseTypes select t).ProjectTo<ExpenseTypeSM>();
      }

      public ExpenseSM UpdateExpense(ExpenseSM expense)
      {
        var exp = _data.Expenses.SingleOrDefault(x => x.Id == expense.Id);
        AssertObjectNotNull(exp);

        exp.ExpenseType = _data.ExpenseTypes.SingleOrDefault(x => x.Id == expense.ExpenseType.Id);
        exp.ExpenseValue = expense.ExpenseValue;
        exp.ExpenseDate = expense.ExpenseDate;

        _data.SaveChanges();

        return expense;
      }

      public ExpenseTypeSM UpdateExpenseType(ExpenseTypeSM expenseType)
      {
        var exp = _data.ExpenseTypes.SingleOrDefault(x => x.Id == expenseType.Id);
        if (exp == null)
        {
          return null;
        }
        exp.ExpenseType = expenseType.ExpenseType;
        _data.SaveChanges();

        return expenseType;

      }
    }
  }

}