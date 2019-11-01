using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CJ.Exp.ServiceModels;

namespace CJ.Exp.API.ApiModels
{
  public class ExpensesFilterAM
  {
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    public int ItemsPerPage { get; set; }
    public int PageNumber { get; set; }
    
  }
}
