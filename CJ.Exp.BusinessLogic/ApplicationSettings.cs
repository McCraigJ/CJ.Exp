using System;
using System.Collections.Generic;
using System.Text;
using CJ.Exp.DomainInterfaces;

namespace CJ.Exp.BusinessLogic
{
  public class ApplicationSettings : IApplicationSettings
  {
    public string ConnectionString { get; set; }
    public string DatabaseName { get; set; }
  }
}
