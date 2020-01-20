using System.ComponentModel.DataAnnotations;

namespace CJ.Exp.Admin.Models.AccountViewModels
{
  public class LoginWithRecoveryCodeViewModel
    {
            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "Recovery Code")]
            public string RecoveryCode { get; set; }
    }
}
