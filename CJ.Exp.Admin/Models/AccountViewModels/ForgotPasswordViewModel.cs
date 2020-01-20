using System.ComponentModel.DataAnnotations;

namespace CJ.Exp.Admin.Models.AccountViewModels
{
  public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
