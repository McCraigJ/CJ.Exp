using AutoMapper;
using CJ.Exp.ServiceModels;
using CJ.Exp.ServiceModels.Auth;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace CJ.Exp.BusinessLogic.Auth
{
  public class ModelMapper : Profile
  {
    public ModelMapper()
    {
      CreateMap<UserSM, ApplicationUser>();
      CreateMap<ApplicationUser, UserSM>();

      CreateMap<IdentityError, ProcessingErrorSM>();
    }
  }
}
