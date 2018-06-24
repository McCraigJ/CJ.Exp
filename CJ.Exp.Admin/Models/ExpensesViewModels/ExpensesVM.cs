using CJ.Exp.ServiceModels.Expenses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CJ.Exp.Admin.Models.ExpensesViewModels
{
  public class ExpensesVM
  {
    public List<ExpenseSM> Expenses { get; set; }
  }
}
