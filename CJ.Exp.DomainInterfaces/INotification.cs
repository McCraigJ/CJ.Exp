using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CJ.Exp.DomainInterfaces
{
  public interface INotification
  {
    Task SendEmailAsync(string email, string subject, string message);
  }
}
