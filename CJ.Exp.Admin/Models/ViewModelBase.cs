using System.Collections.Generic;
using CJ.Exp.DomainInterfaces;
using CJ.Exp.ServiceModels;

namespace CJ.Exp.Admin.Models
{
  public class ViewModelBase
  {
    public List<string> ErrorMessages { get; set; }

    public Dictionary<string, string> Options { get; set; }

    public ViewModelBase()
    {
      ErrorMessages = new List<string>();
    }
    
    public void SetErrorMessage(List<BusinessErrorSM> businessErrors, ILanguage language)
    {
      foreach (var err in businessErrors)
      {
        ErrorMessages.Add(language.GetText(err.ErrorMessage));
      }
    }
  }
}
