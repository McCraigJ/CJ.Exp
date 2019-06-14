using System;
using System.Collections.Generic;
using System.Text;

namespace CJ.Exp.ServiceModels
{
  public class GridRequestSM
  {
    public int PageNumber
    {
      get => PageNumber == 0 ? 1 : PageNumber;
      set => PageNumber = value;
    }

    public int ItemsPerPage { get; set; }

    public int Skip => (PageNumber - 1) * ItemsPerPage;
  }
}
