using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CJ.Exp.Data.Models
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
