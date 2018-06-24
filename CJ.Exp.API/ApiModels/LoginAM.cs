using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CJ.Exp.API.ApiModels
{
  public class LoginAM
  {
    [Required]
    public string Email { get; set; }

    [Required]
    public string Password { get; set; }

  }
}
