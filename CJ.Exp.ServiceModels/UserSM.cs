using System;
using System.Collections.Generic;
using System.Text;

namespace CJ.Exp.ServiceModels
{
  public enum UserDataPiece
  {
    Email = 1,
    Phone = 2
  }

  public class UserSM
  {
    public string Id { get; set; }
    public string Email { get; set; } 
    public string PhoneNumber { get; set; }
  }
}
