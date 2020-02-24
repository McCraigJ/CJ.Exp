using System;

namespace CJ.Exp.ApiModels
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
    public DateTime ExpenseDate { get; set; }
    public decimal ExpenseValue { get; set; }
  }
}
