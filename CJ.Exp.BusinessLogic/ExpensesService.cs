using AutoMapper.QueryableExtensions;
using CJ.Exp.BusinessLogic.Interfaces;
using CJ.Exp.Data;
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

    public ExpenseSM AddExpense()
    {
      throw new NotImplementedException();
    }

    public ExpenseTypeSM AddExpenseType()
    {
      throw new NotImplementedException();
    }

    public bool DeleteExpense()
    {
      throw new NotImplementedException();
    }

    public bool DeleteExpenseType()
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

    public ExpenseSM UpdateExpense()
    {
      throw new NotImplementedException();
    }

    public ExpenseTypeSM UpdateExpenseType()
    {
      throw new NotImplementedException();
    }
  }
}
