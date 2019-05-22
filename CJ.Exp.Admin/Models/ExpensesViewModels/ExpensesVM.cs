using CJ.Exp.ServiceModels.Expenses;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CJ.Exp.Admin.Models.ExpensesViewModels
{

  public class ExpenseVM : ViewModelBase
  {
    public int Id { get; set; }
    
    [Display(Name = "Expense Type")]
    public string ExpenseTypeId { get; set; }
    [Display(Name="New Expense Type")]
    public string NewExpenseType { get; set; }
    [Display(Name = "Value")]
    public decimal ExpenseValue { get; set; }
    [Display(Name = "Date")]
    public DateTime ExpenseDate { get; set; }
    public string User { get; set; }
    public List<SelectListItem> ExpenseTypes { get; set; }
  }

  public class ExpensesVM : ViewModelBase
  {
    public List<ExpenseSM> Expenses { get; set; }
  }
}
