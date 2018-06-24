﻿using Microsoft.AspNetCore.Identity;
using System;

namespace CJ.Exp.ServiceModels.Auth
{
  public class ApplicationUser : IdentityUser
  {
    public string FirstName { get; set; }
    public string LastName { get; set; }

  }
}
