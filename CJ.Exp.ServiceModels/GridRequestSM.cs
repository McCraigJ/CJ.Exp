using System;
using System.Collections.Generic;
using System.Text;

namespace CJ.Exp.ServiceModels
{
  public class GridRequestSM
  {
    private int _pageNumber;
    public int PageNumber
    {
      get => _pageNumber == 0 ? 1 : _pageNumber;
      set => _pageNumber = value;
    }

    public int ItemsPerPage { get; set; }

    public int Skip => (PageNumber - 1) * ItemsPerPage;
  }
}
