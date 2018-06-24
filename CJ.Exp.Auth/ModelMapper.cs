using AutoMapper;
using CJ.Exp.ServiceModels;
using CJ.Exp.ServiceModels.Auth;
using Microsoft.AspNetCore.Identity;

namespace CJ.Exp.BusinessLogic.Auth
{
  public class ModelMapper : Profile
  {
    public ModelMapper()
    {
      CreateMap<UserSM, ApplicationUser>()
       .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email));

      CreateMap<ApplicationUser, UserSM>();

      CreateMap<IdentityError, ProcessingErrorSM>();
    }
  }
}
