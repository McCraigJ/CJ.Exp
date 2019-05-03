using System;
using System.Collections.Generic;
using System.Text;

namespace CJ.Exp.DomainInterfaces
{
  public interface IApplicationSettings
  {
    string ConnectionString { get; set; }
    string DatabaseName { get; set; }
  }
}
