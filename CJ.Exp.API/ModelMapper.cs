using AutoMapper;
using CJ.Exp.API.ApiModels;
using CJ.Exp.ServiceModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CJ.Exp.API
{
  public class ModelMapper : Profile
  {
    public ModelMapper()
    {
      CreateMap<RegisterAM, UserSM>();
    }
  }
}
