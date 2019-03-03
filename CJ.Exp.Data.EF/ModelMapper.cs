using AutoMapper;
using CJ.Exp.Data.EF.DataModels;
using CJ.Exp.ServiceModels;
using CJ.Exp.ServiceModels.Expenses;
using CJ.Exp.ServiceModels.Users;
using Microsoft.AspNetCore.Identity;

namespace CJ.Exp.Data.EF
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
