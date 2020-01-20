using System.ComponentModel.DataAnnotations;

namespace CJ.Exp.Admin.Models.AccountViewModels
{
  public class LoginViewModel : ViewModelBase
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
}
