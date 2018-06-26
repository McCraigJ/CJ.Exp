using CJ.Exp.ServiceModels.Expenses;
using System.Collections.Generic;

namespace CJ.Exp.Admin.Models.ExpensesViewModels
{
  public class ExpenseTypeVM : ViewModelBase
  {
    public int Id { get; set; }
    public string ExpenseType { get; set; }
  }

  public class ExpenseTypesVM : ViewModelBase
  {
    public List<ExpenseTypeSM> ExpenseTypes { get; set; }
  }
}
