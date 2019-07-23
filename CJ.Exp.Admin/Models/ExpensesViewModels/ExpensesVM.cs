using CJ.Exp.ServiceModels.Expenses;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CJ.Exp.ServiceModels;
using Newtonsoft.Json.Converters;

namespace CJ.Exp.Admin.Models.ExpensesViewModels
{

  public class ExpenseVM : ViewModelBase
  {
    public string Id { get; set; }
    
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

  public class ExpensesFilterVM
  {
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public bool IsFiltered { get; set; }
    public ExpenseTypeSM ExpenseType { get; set; }
  }

  public class ExpensesVM : ViewModelBase
  {
    public ExpensesFilterVM Filter { get; set; }
    public GridResultSM<ExpenseSM> ExpenseGrid { get; set; }    
  }

  public class ExpenseRowVM
  {
    public string Date { get; set; }
    public string ExpenseType { get; set; }
    public string ExpenseValue { get; set; }
  }
}
