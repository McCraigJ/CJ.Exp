﻿using System;
using System.ComponentModel.DataAnnotations.Schema;
using CJ.Exp.ServiceModels.Users;

namespace CJ.Exp.Data.EF.DataModels
{
  [Table(name: "Expenses")]
  public class ExpenseDM
  {
    public string Id { get; set; }        
    //public string ExpenseTypeId { get; set; }
    public ExpenseTypeDM ExpenseType { get; set; }
    public decimal ExpenseValue { get; set; }
    public DateTime ExpenseDate { get; set; }    
    public ApplicationUserEf User { get; set; }

    public Guid SyncId { get; set; }
    public DateTime SyncDate { get; set; }
  }
}
