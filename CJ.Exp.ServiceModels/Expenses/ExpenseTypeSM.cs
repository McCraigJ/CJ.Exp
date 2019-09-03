using System;

namespace CJ.Exp.ServiceModels.Expenses
{
  public class ExpenseTypeSM : ServiceModelBase
  {
    public string Id { get; set; }
    public string ExpenseType { get; set; }
    public Guid SyncId { get; set; }
    public DateTime SyncDate { get; set; }
  }

  public class UpdateExpenseTypeSM : ExpenseTypeSM
  {
    public bool UpdateExpensesWithUpdatedType { get; set; }
  }

  [Serializable]
  public class ExpenseTypesFilterSM
  {
    public GridRequestSM GridFilter { get; set; }
  }
}
