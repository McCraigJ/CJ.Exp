using CJ.Exp.ServiceModels.Expenses;
using System.Collections.Generic;

namespace CJ.Exp.Admin.Models.ExpensesViewModels
{
  public class ExpensesVM : ViewModelBase
  {
    public List<ExpenseSM> Expenses { get; set; }
  }
}
