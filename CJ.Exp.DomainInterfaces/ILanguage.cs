using System;
using System.Collections.Generic;
using System.Text;

namespace CJ.Exp.DomainInterfaces
{
  public interface ILanguage
  {
    void Initialise(bool checkLastModified = false);

    string GetText(string path);
  }
}
