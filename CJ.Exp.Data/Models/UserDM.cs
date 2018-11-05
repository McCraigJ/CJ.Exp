using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CJ.Exp.Data.Models
{
  [Table(name: "AspNetUsers")]
  public class UserDM
  {
    public Guid Id { get; set; }
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public bool IsLockedOut { get; set; }
    public string PhoneNumber { get; set; }
  }
}
