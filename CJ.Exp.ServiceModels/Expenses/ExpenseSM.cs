using CJ.Exp.ServiceModels.Users;
using System;
using System.Collections.Generic;

namespace CJ.Exp.ServiceModels.Expenses
{
  public class ExpenseSM : ServiceModelBase
  {
    public string Id { get; set; }
    public ExpenseTypeSM ExpenseType { get; set; }
    
    public decimal ExpenseValue { get; set; }
    public DateTime ExpenseDate { get; set; }
    public UserSM User { get; set; }
    public Guid SyncId { get; set; }
    public DateTime SyncDate { get; set; }
  }

  public class UpdateExpenseSM : ExpenseSM
  {
    public string NewExpenseType { get; set; }
  }

  [Serializable]
  public class ExpensesFilterSM : SearchFilterBaseSM
  {
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public ExpenseTypeSM ExpenseType { get; set; }
    
  }

  [Serializable]
  public class SearchFilterBaseSM
  {
    public GridRequestSM GridFilter { get; set; }
  }
}
