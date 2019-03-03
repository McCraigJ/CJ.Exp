using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CJ.Exp.Data.EF.DataModels
{
  [Table(name: "ExpenseTypes")]
  public class ExpenseTypeDM
  {
    public int Id { get; set; }
    public string ExpenseType { get; set; }
    public Guid SyncId { get; set; }
    public DateTime SyncDate { get; set; }
  }
}
