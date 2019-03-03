using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CJ.Exp.Data.EF.DataModels
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
