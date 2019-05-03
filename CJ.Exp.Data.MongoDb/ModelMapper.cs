using AutoMapper;
using CJ.Exp.Data.MongoDb.User;
using CJ.Exp.ServiceModels.Users;

namespace CJ.Exp.Data.MongoDb
{
  public class ModelMapper : Profile
  {
    public ModelMapper()
    {
      CreateMap<UserSM, ApplicationUserMongo>()
        .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email));
    }    
  }
}
