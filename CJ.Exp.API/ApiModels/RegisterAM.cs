using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CJ.Exp.API.ApiModels
{
  public class RegisterAM
  {
    [Required]
    public string Email { get; set; }

    [Required]
    [StringLength(100, ErrorMessage = "PASSWORD_MIN_LENGTH", MinimumLength = 6)]
    public string Password { get; set; }
  }
}
