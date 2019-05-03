using System;

namespace CJ.Exp.ServiceModels.Expenses
{
  public class ExpenseTypeSM
  {
    public string Id { get; set; }
    public string ExpenseType { get; set; }
    public Guid SyncId { get; set; }
    public DateTime SyncDate { get; set; }
  }
}
