using System;
using CJ.Exp.Core;
using CJ.Exp.Data.Interfaces;
using CJ.Exp.DomainInterfaces;
using CJ.Exp.ServiceModels.Expenses;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using CJ.Exp.ServiceModels;

namespace CJ.Exp.BusinessLogic.Expenses
{
  public class ExpensesService : IExpensesService
  {
    private IExpensesData _data;

    public ExpensesService(IExpensesData data)
    {
      _data = data;
    }

    public ExpenseSM GetExpenseById(string id)
    {
      return _data.GetExpenseById(id);
    }

    public ServiceResponse<UpdateExpenseSM> AddExpense(UpdateExpenseSM expense)
    {      
      if (string.IsNullOrEmpty(expense.NewExpenseType))
      {
        expense.ExpenseType = _data.GetExpenseTypeById(expense.ExpenseType.Id);
      }
      else {

        if (_data.GetExpenseTypeByName(expense.NewExpenseType) != null)
        {
          return new ServiceResponse<UpdateExpenseSM>(expense, ServiceResponseCode.DataAlreadyExists);
        }

        expense.ExpenseType = new ExpenseTypeSM
        {
          Id = null,
          ExpenseType = expense.NewExpenseType
        };
      }

      var exp = _data.AddExpense(expense);
      expense.Id = exp.Id;

      return new ServiceResponse<UpdateExpenseSM>(expense, ServiceResponseCode.Success);
    }

    public ExpenseSM UpdateExpense(UpdateExpenseSM expense)
    {
      throw new System.NotImplementedException();
    }

    public ServiceResponse<ExpenseTypeSM> AddExpenseType(ExpenseTypeSM expenseType)
    {      
      if (_data.GetExpenseTypeByName(expenseType.ExpenseType) != null)
      {        
        return new ServiceResponse<ExpenseTypeSM>(expenseType, ServiceResponseCode.DataAlreadyExists);
      }

      expenseType = _data.AddExpenseType(expenseType);
      return new ServiceResponse<ExpenseTypeSM>(expenseType, ServiceResponseCode.Success);
    }    

    public bool DeleteExpense(ExpenseSM expense)
    {
      return _data.DeleteExpense(expense);
    }

    public bool DeleteExpenseType(ExpenseTypeSM expenseType)
    {
      return _data.DeleteExpenseType(expenseType);
    }

    // Todo: remove gridRequest
    public GridResultSM<ExpenseSM> GetExpenses(ExpensesFilterSM filter, GridRequestSM gridRequest)
    {
      // todo: remove
      gridRequest = filter.GridFilter;
      if (gridRequest == null)
      {
        throw new ApplicationException("No Grid Request Data has been supplied");
      }
      return _data.GetExpenses(filter, gridRequest);
    }

    public List<ExpenseTypeSM> GetExpenseTypes()
    {
      return _data.GetExpenseTypes();
    }    

    public ExpenseSM UpdateExpense(ExpenseSM expense)
    {
      return _data.UpdateExpense(expense);
    }

    public ServiceResponse<UpdateExpenseTypeSM> UpdateExpenseType(UpdateExpenseTypeSM expenseType)
    {
      _data.StartTransaction();
      _data.UpdateExpenseType(expenseType);

      if (expenseType.UpdateExpensesWithUpdatedType)
      {
        _data.UpdateExpenseWithUpdatedExpenseType(expenseType);
      }
      _data.CommitTransaction();

      return new ServiceResponse<UpdateExpenseTypeSM>(expenseType, ServiceResponseCode.Success);
    }
  }
}
