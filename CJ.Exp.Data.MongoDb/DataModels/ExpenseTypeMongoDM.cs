using System;
using System.Collections.Generic;
using System.Text;

namespace CJ.Exp.Data.MongoDb.DataModels
{
  public class ExpenseTypeMongoDM
  {
    public Guid Id { get; set; }
    public string ExpenseType { get; set; }
    public Guid SyncId { get; set; }
    public DateTime SyncDate { get; set; }
  }
}
