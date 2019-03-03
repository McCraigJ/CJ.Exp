using CJ.Exp.ServiceModels.Users;
using System;

namespace CJ.Exp.ServiceModels.Expenses
{
  public class ExpenseSM
    {
    public int Id { get; set; }
    //public string ExpenseTypeId { get; set; }
    public ExpenseTypeSM ExpenseType { get; set; }
    public decimal ExpenseValue { get; set; }
    public DateTime ExpenseDate { get; set; }
    public UserSM User { get; set; }
    public Guid SyncId { get; set; }
    public DateTime SyncDate { get; set; }
  }
}
