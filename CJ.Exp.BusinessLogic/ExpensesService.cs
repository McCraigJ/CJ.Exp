using AutoMapper.QueryableExtensions;
using CJ.Exp.BusinessLogic.Interfaces;
using CJ.Exp.Data;
using CJ.Exp.Data.Models;
using CJ.Exp.ServiceModels.Expenses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CJ.Exp.BusinessLogic
{

  public class ExpensesService : IExpensesService
  {
    private readonly ExpDbContext _data;

    public ExpensesService(ExpDbContext data)
    {
      _data = data;
    }

    public ExpenseSM AddExpense(ExpenseSM expense)
    {
      throw new NotImplementedException();
    }

    public ExpenseTypeSM AddExpenseType(ExpenseTypeSM expenseType)
    {
      var exp = AutoMapper.Mapper.Map<ExpenseTypeDM>(expenseType);      
      _data.ExpenseTypes.Add(exp);
      _data.SaveChanges();
      expenseType.Id = exp.Id;
      return expenseType;
    }

    public bool DeleteExpense()
    {
      throw new NotImplementedException();
    }

    public bool DeleteExpenseType(ExpenseTypeSM expenseType)
    {
      throw new NotImplementedException();
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
      throw new NotImplementedException();
    }

    public ExpenseTypeSM UpdateExpenseType(ExpenseTypeSM expenseType)
    {
      throw new NotImplementedException();
    }
  }
}
