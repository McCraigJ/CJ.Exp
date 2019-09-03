using CJ.Exp.ServiceModels.Expenses;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CJ.Exp.Admin.Models.ExpensesViewModels
{
  public class ExpenseTypeVM : ViewModelBase
  {
    public string Id { get; set; }
    [Display(Name = "Expense Type")]
    public string ExpenseType { get; set; }
    [Display(Name = "Change exiting expense records")]
    public bool UpdateExpensesWithUpdatedType { get; set; }
  }

  public class ExpenseTypesVM : ViewModelBase
  {
    //public List<ExpenseTypeSM> ExpenseTypes { get; set; }
  }
}
