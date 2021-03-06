﻿using AutoMapper;
using CJ.Exp.API.ApiModels;
using CJ.Exp.ServiceModels.Expenses;
using CJ.Exp.ServiceModels.Users;

namespace CJ.Exp.API
{
  public class ModelMapper : Profile
  {
    public ModelMapper()
    {
      CreateMap<RegisterAM, UserSM>();

      CreateMap<AddExpenseAM, UpdateExpenseSM>();
    }
  }
}
