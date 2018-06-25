using AutoMapper;
using CJ.Exp.Data.Models;
using CJ.Exp.ServiceModels;
using CJ.Exp.ServiceModels.Expenses;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace CJ.Exp.Data
{
  public class ModelMapper : Profile
  {
    public ModelMapper()
    {

      CreateMap<UserSM, ApplicationUser>()
       .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email));

      CreateMap<ApplicationUser, UserSM>();

      CreateMap<IdentityError, ProcessingErrorSM>();

      CreateMap<ExpenseTypeDM, ExpenseTypeSM>();
      CreateMap<ExpenseTypeSM, ExpenseTypeDM>();

      CreateMap<ExpenseDM, ExpenseSM>();
      CreateMap<ExpenseSM, ExpenseDM>();

    }
  }
}
