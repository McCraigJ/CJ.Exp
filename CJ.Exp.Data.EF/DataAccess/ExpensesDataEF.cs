using System;
using AutoMapper.QueryableExtensions;
using CJ.Exp.ServiceModels.Expenses;
using System.Collections.Generic;
using System.Threading.Tasks;
using CJ.Exp.ServiceModels;
using Microsoft.EntityFrameworkCore;

namespace CJ.Exp.Data.EF.DataAccess
{
  using AutoMapper;
  using global::CJ.Exp.Data.EF.DataModels;
  using global::CJ.Exp.Data.Interfaces;
  using System.Linq;

  namespace CJ.Exp.BusinessLogic
  {

    public class ExpensesDataEf : DataEfAccessBase, IExpensesData
    {
      public ExpensesDataEf(ExpDbContext data) : base(data) { }

      public async Task<ExpenseSM> AddExpenseAsync(ExpenseSM expense)
      {
        var exp = Mapper.Map<ExpenseDM>(expense);
        exp.ExpenseType = await _data.ExpenseTypes.SingleOrDefaultAsync(x => x.Id == Convert.ToInt32(expense.ExpenseType.Id));
        _data.Expenses.Add(exp);
        await _data.SaveChangesAsync();
        expense.Id = exp.Id;
        return expense;
      }

      public async Task<ExpenseTypeSM> AddExpenseTypeAsync(ExpenseTypeSM expenseType)
      {
        var exp = Mapper.Map<ExpenseTypeDM>(expenseType);
        _data.ExpenseTypes.Add(exp);
        await _data.SaveChangesAsync();
        expenseType.Id = exp.Id.ToString();
        return expenseType;
      }

      public async Task<bool> DeleteExpenseAsync(ExpenseSM expense)
      {
        var exp = await _data.Expenses.SingleOrDefaultAsync(x => x.Id == expense.Id);
        if (exp == null)
        {
          return false;
        }
        _data.Remove(exp);
        await _data.SaveChangesAsync();

        return true;

      }

      public async Task<bool> DeleteExpenseTypeAsync(ExpenseTypeSM expenseType)
      {
        var exp = await _data.ExpenseTypes.SingleOrDefaultAsync(x => x.Id == Convert.ToInt32(expenseType.Id));
        if (exp == null)
        {
          return false;
        }
        _data.Remove(exp);
        await _data.SaveChangesAsync();

        return true;

      }

      public Task<GridResultSM<ExpenseSM>> GetExpensesAsync(ExpensesFilterSM filter)
      {
        throw new NotImplementedException();
      }

      public Task<GridResultSM<ExpenseSM>> GetExpensesAsync(ExpensesFilterSM filter, GridRequestSM gridRequest)
      {
        throw new NotImplementedException();
      }

      public Task<ExpenseSM> GetExpenseByIdAsync(string id)
      {
        throw new NotImplementedException();
      }

      public Task<GridResultSM<ExpenseTypeSM>> GetExpenseTypesAsync(ExpenseTypesFilterSM filter)
      {
        throw new NotImplementedException();
      }

      public Task<List<ExpenseTypeSM>> GetExpenseTypesAsync()
      {
        throw new NotImplementedException();
      }

      public Task<ExpenseTypeSM> GetExpenseTypeByIdAsync(string id)
      {
        throw new NotImplementedException();
      }

      public Task<ExpenseTypeSM> GetExpenseTypeByNameAsync(string expenseTypeName)
      {
        throw new NotImplementedException();
      }

      public async Task<ExpenseSM> UpdateExpenseAsync(ExpenseSM expense)
      {
        var exp = await _data.Expenses.SingleOrDefaultAsync(x => x.Id == expense.Id);
        AssertObjectNotNull(exp);

        exp.ExpenseType = await _data.ExpenseTypes.SingleOrDefaultAsync(x => x.Id == Convert.ToInt32(expense.ExpenseType.Id));
        exp.ExpenseValue = expense.ExpenseValue;
        exp.ExpenseDate = expense.ExpenseDate;

        await _data.SaveChangesAsync();

        return expense;
      }

      public async Task<ExpenseTypeSM> UpdateExpenseTypeAsync(ExpenseTypeSM expenseType)
      {
        var exp = await _data.ExpenseTypes.SingleOrDefaultAsync(x => x.Id == Convert.ToInt32(expenseType.Id));
        if (exp == null)
        {
          return null;
        }
        exp.ExpenseType = expenseType.ExpenseType;
        await _data.SaveChangesAsync();

        return expenseType;

      }

      public Task<bool> UpdateExpenseWithUpdatedExpenseTypeAsync(ExpenseTypeSM expenseType)
      {
        throw new NotImplementedException();
      }

      public void StartTransaction()
      {
        throw new NotImplementedException();
      }

      public void CommitTransaction()
      {
        throw new NotImplementedException();
      }

      public void RollbackTransaction()
      {
        throw new NotImplementedException();
      }
    }
  }

}
