using System;
using System.Collections.Generic;
using System.Text;

namespace CJ.Exp.DomainInterfaces
{
  public interface IApplicationUser
  {
    string ApplicationId { get; set; }
    string UserName { get; set; }
    string Email { get; set; }
    string FirstName { get; set; }
    string LastName { get; set; }

  }

  public interface IApplicationRole
  {
    string Name { get; set; }
  }
}
