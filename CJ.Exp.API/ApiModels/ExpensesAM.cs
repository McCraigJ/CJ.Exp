using System;
using System.ComponentModel.DataAnnotations;

namespace CJ.Exp.API.ApiModels
{
  public class ExpensesFilterAM
  {
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    public int ItemsPerPage { get; set; }
    public int PageNumber { get; set; }
    
  }

  public class AddExpenseAM
  {
    public string ExpenseTypeId { get; set; }
    public string NewExpenseType { get; set; }
    [Required]
    public DateTime ExpenseDate { get; set; }
    [Required]
    public decimal ExpenseValue { get; set; }
  }
}
