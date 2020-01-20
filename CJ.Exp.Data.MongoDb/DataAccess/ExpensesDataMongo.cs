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
using System.Threading.Tasks;
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

    public async Task<ExpenseSM> AddExpenseAsync(ExpenseSM expense)
    {

      StartTransaction();

      if (string.IsNullOrEmpty(expense.ExpenseType.Id))
      {
        await AddExpenseTypeAsync(expense.ExpenseType);
      }

      var dm = Mapper.Map<ExpenseMongoDM>(expense);
      await _expenseCollection.InsertOneAsync(dm);
      expense.Id = dm.Id.ToString();
      
      CommitTransaction();

      return expense;
    }

    public async Task<ExpenseTypeSM> AddExpenseTypeAsync(ExpenseTypeSM expenseType)
    {            
      var dm = Mapper.Map<ExpenseTypeMongoDM>(expenseType);
      await _expenseTypeCollection.InsertOneAsync(dm);
      expenseType.Id = dm.Id.ToString();
      return expenseType;            
    }
    

    public async Task<bool> DeleteExpenseAsync(ExpenseSM expense)
    {
      await _expenseCollection.FindOneAndDeleteAsync<ExpenseMongoDM>(Builders<ExpenseMongoDM>.Filter.Eq("_id", new ObjectId(expense.Id)));
      return true;
    }

    public async Task<bool> DeleteExpenseTypeAsync(ExpenseTypeSM expenseType)
    {
      await _expenseTypeCollection.FindOneAndDeleteAsync<ExpenseTypeMongoDM>(Builders<ExpenseTypeMongoDM>.Filter.Eq("_id", new ObjectId(expenseType.Id)));
      return true;
    }

    public async Task<GridResultSM<ExpenseSM>> GetExpensesAsync(ExpensesFilterSM filter)
    {
      if (filter?.GridFilter == null)
      {
        return null;
      }

      var query = _expenseCollection.Find(x => x.ExpenseDate >= filter.StartDate && x.ExpenseDate <= filter.EndDate.AddDays(1));

      List<ExpenseMongoDM> expenses;
      
      expenses = await query.Skip(filter.GridFilter.Skip).Limit(filter.GridFilter.ItemsPerPage).ToListAsync();
      
      var sum = await _expenseCollection.Aggregate()
        .Group(
            x => null as string, //x.Id,
            group => new
        {
          Id = group.Key,
          Sum = group.Sum(x => x.ExpenseValue)
        }).SingleOrDefaultAsync();

      var count = await _expenseCollection.CountDocumentsAsync(x => true);

      return new GridResultSM<ExpenseSM>(filter.GridFilter.PageNumber, 
        (int)count,
        filter.GridFilter.ItemsPerPage, 
        sum.Sum / 1000m, 
        Mapper.Map<List<ExpenseSM>>(expenses));
    }

    public async Task<ExpenseSM> GetExpenseByIdAsync(string id)
    {
      var expense = await _expenseCollection.Find(x => x.Id == new ObjectId(id)).SingleOrDefaultAsync();
      return Mapper.Map<ExpenseSM>(expense);
    }

    public async Task<GridResultSM<ExpenseTypeSM>> GetExpenseTypesAsync(ExpenseTypesFilterSM filter)
    {
      if (filter?.GridFilter == null)
      {
        return null;
      }

      var query = _expenseTypeCollection.Find(_ => true).SortBy(x => x.ExpenseType);

      var expenseTypes = await query.Skip(filter.GridFilter.Skip).Limit(filter.GridFilter.ItemsPerPage).ToListAsync();

      var count = await _expenseCollection.CountDocumentsAsync(x => true);

      return new GridResultSM<ExpenseTypeSM>(filter?.GridFilter?.PageNumber ?? 0,
        (int)count,
        filter?.GridFilter?.ItemsPerPage ?? 0,
        null,
        Mapper.Map<List<ExpenseTypeSM>>(expenseTypes));
    }

    public async Task<List<ExpenseTypeSM>> GetExpenseTypesAsync()
    {
      var expenseTypes = await _expenseTypeCollection.Find(_ => true).SortBy(x => x.ExpenseType).ToListAsync();
      return Mapper.Map<List<ExpenseTypeSM>>(expenseTypes);

    }

    public async Task<ExpenseTypeSM> GetExpenseTypeByIdAsync(string id)
    {
      var type = await _expenseTypeCollection.Find(x => x.Id == new ObjectId(id)).SingleOrDefaultAsync();
      if (type == null)
      {
        return null;
      }
      return Mapper.Map<ExpenseTypeSM>(type);
    }

    public async Task<ExpenseTypeSM> GetExpenseTypeByNameAsync(string expenseTypeName)
    {
      var type = await _expenseTypeCollection.Find(x => x.ExpenseType == expenseTypeName).SingleOrDefaultAsync();
      if (type == null)
      {
        return null;
      }
      return Mapper.Map<ExpenseTypeSM>(type);
    }

    public async Task<ExpenseSM> UpdateExpenseAsync(ExpenseSM expense)
    {
      var expDataModel = Mapper.Map<ExpenseMongoDM>(expense);

      await _expenseCollection.FindOneAndUpdateAsync<ExpenseMongoDM>(
        Builders<ExpenseMongoDM>.Filter.Eq("_id", new ObjectId(expense.Id)),
        Builders<ExpenseMongoDM>.Update
          .Set("ExpenseType", expDataModel.ExpenseType)
          .Set("ExpenseValue", expDataModel.ExpenseValue)
          .Set("ExpenseDate", expDataModel.ExpenseDate)
          .Set("User", expDataModel.User)
      );
      
      return expense;
    }

    public async Task<ExpenseTypeSM> UpdateExpenseTypeAsync(ExpenseTypeSM expenseType)
    {
      await _expenseTypeCollection.FindOneAndUpdateAsync<ExpenseTypeMongoDM>(
        Builders<ExpenseTypeMongoDM>.Filter.Eq("_id", new ObjectId(expenseType.Id)),
        Builders<ExpenseTypeMongoDM>.Update.Set("ExpenseType", expenseType.ExpenseType)
      );
      
      return expenseType;
    }

    public async Task<bool> UpdateExpenseWithUpdatedExpenseTypeAsync(ExpenseTypeSM expenseType)
    {
      await _expenseCollection.UpdateManyAsync(Builders<ExpenseMongoDM>.Filter.Eq("ExpenseType.Id", new ObjectId(expenseType.Id)),
        Builders<ExpenseMongoDM>.Update.Set("ExpenseType.ExpenseType", expenseType.ExpenseType));

      return true;
    }
  }
}
