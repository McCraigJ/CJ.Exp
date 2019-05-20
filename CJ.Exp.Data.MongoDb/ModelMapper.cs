﻿using AutoMapper;
using CJ.Exp.Data.MongoDb.DataModels;
using CJ.Exp.ServiceModels.Expenses;
using CJ.Exp.ServiceModels.Users;
using MongoDB.Bson;

namespace CJ.Exp.Data.MongoDb
{
  public class ModelMapper : Profile
  {
    public ModelMapper()
    {
      CreateMap<UserSM, ApplicationUserMongo>()
        .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email));

      CreateMap<ExpenseTypeMongoDM, ExpenseTypeSM>()
        .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));

      CreateMap<ExpenseTypeSM, ExpenseTypeMongoDM>()
        .ForMember(dest => dest.Id, opt => opt.MapFrom(src => new ObjectId(src.Id)));
    }    
  }
}