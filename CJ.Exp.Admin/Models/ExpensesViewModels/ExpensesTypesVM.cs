using CJ.Exp.ServiceModels.Expenses;
using System.Collections.Generic;

namespace CJ.Exp.Admin.Models.ExpensesViewModels
{
  public class ExpenseTypeVM : ViewModelBase
  {
    public string Id { get; set; }
    public string ExpenseType { get; set; }
    public bool UpdateExpensesWithUpdatedType { get; set; }
  }

  public class ExpenseTypesVM : ViewModelBase
  {
    public List<ExpenseTypeSM> ExpenseTypes { get; set; }
  }
}
