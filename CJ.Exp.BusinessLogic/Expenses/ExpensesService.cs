using AutoMapper;
using AutoMapper.QueryableExtensions;
using CJ.Exp.BusinessLogic.Interfaces;
using CJ.Exp.Data;
using CJ.Exp.Data.Models;
using CJ.Exp.ServiceModels.Expenses;
using System;
using System.Linq;

namespace CJ.Exp.BusinessLogic
{

  public class ExpensesService : IExpensesService
  {
    private IExpensesData _data;
    public ExpensesService(IExpensesData data) { }

    public ExpenseSM AddExpense(ExpenseSM expense)
    {
      return _data.AddExpense(expense);      
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
      try
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
      catch (Exception ex)
      {
        return false;
      }
    }

    public bool DeleteExpenseType(ExpenseTypeSM expenseType)
    {
      try
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
      catch (Exception ex)
      {
        return false;
      }
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
