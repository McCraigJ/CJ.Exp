using System;
using System.Collections.Generic;
using System.Text;

namespace CJ.Exp.ServiceModels.Expenses
{
  public class ExpenseTypeSM
  {
    public int Id { get; set; }
    public string ExpenseType { get; set; }
    public Guid SyncId { get; set; }
    public DateTime SyncDate { get; set; }
  }
}
