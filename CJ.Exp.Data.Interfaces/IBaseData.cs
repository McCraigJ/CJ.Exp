using System;
using System.Collections.Generic;
using System.Text;

namespace CJ.Exp.Data.Interfaces
{
  public interface IBaseData
  {
    void StartTransaction();

    void CommitTransaction();

    void RollbackTransaction();
  }
}
