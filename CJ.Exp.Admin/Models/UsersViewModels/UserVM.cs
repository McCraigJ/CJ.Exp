using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CJ.Exp.Admin.Models.UsersViewModels
{
  public class UserVM : ViewModelBase
  {
    [Required]
    [Display(Name="Email")]
    [EmailAddress]
    public string Email { get; set; }
    [Required]
    [Display(Name = "First Name")]
    public string FirstName { get; set; }
    [Required]
    [Display(Name = "Last Name")]
    public string LastName { get; set; }

    public string Id { get; set; }
  }

  public class UsersVM : ViewModelBase
  {
  }

  public class AddUserVM : UserVM
  {    
    [Required]
    [Display(Name = "Password")]
    public string Password { get; set; }

    [Compare("Password")]
    [Display(Name = "Confirm Password")]
    public string ConfirmPassword { get; set; }

    [Required]
    [Display(Name = "Role")]
    public string Role { get; set; }

    public List<SelectListItem> Roles { get; set; }
  }

  public class EditUserVM : UserVM
  {
    [Required]
    [Display(Name = "Role")]
    public string Role { get; set; }

    public List<SelectListItem> Roles { get; set; }
  }
}
