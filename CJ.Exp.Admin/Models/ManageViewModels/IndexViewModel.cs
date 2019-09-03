using System.ComponentModel.DataAnnotations;

namespace CJ.Exp.Admin.Models.ManageViewModels
{
  public class IndexViewModel : ViewModelBase
  {  
    public bool IsEmailConfirmed { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }    
   
    [Required]
    [Display(Name = "First Name")]
    public string FirstName { get; set; }

    [Required]
    [Display(Name = "Last Name")]
    public string LastName { get; set; }

    public string StatusMessage { get; set; }
  }
}
