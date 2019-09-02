using System;
using System.Threading.Tasks;
using CJ.Exp.DomainInterfaces;

namespace CJ.Exp.Notification
{
  public class EmailSender : INotification
  {
    public Task SendEmailAsync(string email, string subject, string message)
    {
      return Task.CompletedTask;
    }
  }
}
