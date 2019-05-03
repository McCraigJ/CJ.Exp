using AutoMapper;
using CJ.Exp.Data.Interfaces;
using CJ.Exp.Data.MongoDb.DataModels;
using CJ.Exp.Data.MongoDb.Interfaces;
using CJ.Exp.DomainInterfaces;
using CJ.Exp.ServiceModels.Expenses;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CJ.Exp.Data.MongoDb.DataAccess
{
  public class ExpensesDataMongo : DataMongoAccessBase<ExpenseTypeMongoDM>, IExpensesData
  {

    public ExpensesDataMongo(IAppMongoClient mongoClient, IApplicationSettings applicationSettings) : 
      base(mongoClient, applicationSettings, "expensetypes")
    {
    }

    public ExpenseSM AddExpense(ExpenseSM expense)
    {      
      throw new NotImplementedException();
    }

    public ExpenseTypeSM AddExpenseType(ExpenseTypeSM expenseType)
    {
      var dm = Mapper.Map<ExpenseTypeMongoDM>(expenseType);
      Collection.InsertOne(dm);
      expenseType.Id = dm.Id.ToString();
      return expenseType;
    }

    public bool DeleteExpense(ExpenseSM expense)
    {
      throw new NotImplementedException();
    }

    public bool DeleteExpenseType(ExpenseTypeSM expenseType)
    {
      throw new NotImplementedException();
    }

    public List<ExpenseSM> GetExpenses()
    {
      throw new NotImplementedException();
    }

    public List<ExpenseTypeSM> GetExpenseTypes()
    {
      var types = Collection.Find(_ => true).ToList();
      return Mapper.Map<List<ExpenseTypeSM>>(types);
    }

    public ExpenseSM UpdateExpense(ExpenseSM expense)
    {
      throw new NotImplementedException();
    }

    public ExpenseTypeSM UpdateExpenseType(ExpenseTypeSM expenseType)
    {
      Collection.FindOneAndUpdate<ExpenseTypeMongoDM>(
        Builders<ExpenseTypeMongoDM>.Filter.Eq("_id", expenseType.Id),
        Builders<ExpenseTypeMongoDM>.Update.Set("ExpenseType", expenseType.ExpenseType)
      );

      var dm = Mapper.Map<ExpenseTypeMongoDM>(expenseType);
      Collection.InsertOne(dm);
      expenseType.Id = dm.Id.ToString();
      return expenseType;
    }

    
  }
}
