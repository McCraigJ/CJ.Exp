﻿using AutoMapper;
using CJ.Exp.Data.Interfaces;
using CJ.Exp.Data.MongoDb.DataModels;
using CJ.Exp.Data.MongoDb.Interfaces;
using CJ.Exp.DomainInterfaces;
using CJ.Exp.ServiceModels.Expenses;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using CJ.Exp.ServiceModels;
using MongoDB.Bson;

namespace CJ.Exp.Data.MongoDb.DataAccess
{
  public class ExpensesDataMongo : DataMongoAccessBase, IExpensesData
  {

    private readonly IMongoCollection<ExpenseTypeMongoDM> _expenseTypeCollection;
    private readonly IMongoCollection<ExpenseMongoDM> _expenseCollection;

    private class SumResult
    {
      private string Id { get; set; }
      public int Sum { get; set; }
    }

    public ExpensesDataMongo(IAppMongoClient mongoClient, IApplicationSettings applicationSettings) : 
      base(mongoClient, applicationSettings)
    {      
      _expenseTypeCollection = Database.GetCollection<ExpenseTypeMongoDM>("expensetypes");
      _expenseCollection = Database.GetCollection<ExpenseMongoDM>("expenses");
    }

    public ExpenseSM AddExpense(ExpenseSM expense)
    {

      StartTransaction();

      if (string.IsNullOrEmpty(expense.ExpenseType.Id))
      {
        AddExpenseType(expense.ExpenseType);
      }

      var dm = Mapper.Map<ExpenseMongoDM>(expense);
      _expenseCollection.InsertOne(dm);
      expense.Id = dm.Id.ToString();
      
      CommitTransaction();

      return expense;
    }

    public ExpenseTypeSM AddExpenseType(ExpenseTypeSM expenseType)
    {            
      var dm = Mapper.Map<ExpenseTypeMongoDM>(expenseType);
      _expenseTypeCollection.InsertOne(dm);
      expenseType.Id = dm.Id.ToString();
      return expenseType;            
    }
    

    public bool DeleteExpense(ExpenseSM expense)
    {
      throw new NotImplementedException();
    }

    public bool DeleteExpenseType(ExpenseTypeSM expenseType)
    {
      _expenseTypeCollection.FindOneAndDelete<ExpenseTypeMongoDM>(Builders<ExpenseTypeMongoDM>.Filter.Eq("_id", new ObjectId(expenseType.Id)));
      return true;
    }

    public ExpenseSummarySM GetExpenses(ExpensesFilterSM filter, GridRequestSM gridRequest)
    {

      var query = _expenseCollection.Find(x => x.ExpenseDate >= filter.StartDate && x.ExpenseDate <= filter.EndDate.AddDays(1));

      List<ExpenseMongoDM> expenses;
      if (gridRequest == null)
      {
        expenses = query.ToList();
      }
      else
      {
        expenses = query.Skip(gridRequest.Skip).Limit(gridRequest.ItemsPerPage).ToList();
      }

      var sum = _expenseCollection.Aggregate()
        .Group(
            x => null as string, //x.Id,
            group => new
        {
          Id = group.Key,
          Sum = group.Sum(x => x.ExpenseValue)
        }).ToList();

      return new ExpenseSummarySM
      {
        Expenses = Mapper.Map<List<ExpenseSM>>(expenses),
        Total = 0m
      };
    }

    public ExpenseSM GetExpenseById(string id)
    {
      var expense = _expenseCollection.Find(x => x.Id == new ObjectId(id)).SingleOrDefault();
      return Mapper.Map<ExpenseSM>(expense);
    }

    public List<ExpenseTypeSM> GetExpenseTypes()
    {           
      var types = _expenseTypeCollection.Find(_ => true).SortBy(x => x.ExpenseType).ToList();
      return Mapper.Map<List<ExpenseTypeSM>>(types);
    }

    public ExpenseTypeSM GetExpenseTypeById(string id)
    {
      var type = _expenseTypeCollection.Find(x => x.Id == new ObjectId(id)).SingleOrDefault();
      if (type == null)
      {
        return null;
      }
      return Mapper.Map<ExpenseTypeSM>(type);
    }

    public ExpenseTypeSM GetExpenseTypeByName(string expenseTypeName)
    {
      var type = _expenseTypeCollection.Find(x => x.ExpenseType == expenseTypeName).SingleOrDefault();
      if (type == null)
      {
        return null;
      }
      return Mapper.Map<ExpenseTypeSM>(type);
    }

    public ExpenseSM UpdateExpense(ExpenseSM expense)
    {
      throw new NotImplementedException();
    }

    public ExpenseTypeSM UpdateExpenseType(ExpenseTypeSM expenseType)
    {
      _expenseTypeCollection.FindOneAndUpdate<ExpenseTypeMongoDM>(
        Builders<ExpenseTypeMongoDM>.Filter.Eq("_id", new ObjectId(expenseType.Id)),
        Builders<ExpenseTypeMongoDM>.Update.Set("ExpenseType", expenseType.ExpenseType)
      );
      
      return expenseType;
    }

    public bool UpdateExpenseWithUpdatedExpenseType(ExpenseTypeSM expenseType)
    {
      var expenseDocs = _expenseCollection.Find(x => x.ExpenseType.Id == new ObjectId(expenseType.Id));

      _expenseCollection.UpdateMany(Builders<ExpenseMongoDM>.Filter.Eq("ExpenseType.Id", new ObjectId(expenseType.Id)),
        Builders<ExpenseMongoDM>.Update.Set("ExpenseType.ExpenseType", expenseType.ExpenseType));

      return true;
    }
  }
}
