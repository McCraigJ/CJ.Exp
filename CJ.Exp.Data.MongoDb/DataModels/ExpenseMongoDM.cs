using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson;

namespace CJ.Exp.Data.MongoDb.DataModels
{
  public class ExpenseMongoDM
  {
    public ObjectId Id { get; set; }
    public ExpenseTypeMongoBaseDM ExpenseType { get; set; }
    public decimal ExpenseValue { get; set; }
    public DateTime ExpenseDate { get; set; }
    public MongoUserDetails User { get; set; }
    public Guid SyncId { get; set; }
    public DateTime SyncDate { get; set; }
  }

  public class MongoUserDetails
  {
    public Guid Id { get; set; } 
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
  }

  public class ExpenseTypeDetails
  {
    
  }
}
