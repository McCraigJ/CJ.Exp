using System;
using System.Collections.Generic;

namespace CJ.Exp.ServiceModels
{
  public class GridResultSM<T> where T : ServiceModelBase
  {
    public int CurrentPageNumber { get; }
    public int TotalRecordCount { get; }
    
    public int RecordsPerPage { get; }

    public int TotalPages => (int) Math.Ceiling(TotalRecordCount / (decimal) RecordsPerPage);

    public decimal? GridPageTotal { get;  }
    public List<T> GridRows { get; }


    public GridResultSM(int currentPageNumber, int totalRecordCount, int recordsPerPage, decimal? gridPageTotal, List<T> gridRows)
    {
      CurrentPageNumber = currentPageNumber;
      TotalRecordCount = totalRecordCount;
      RecordsPerPage = recordsPerPage;
      GridPageTotal = gridPageTotal;
      GridRows = gridRows;
    }
  }
}
