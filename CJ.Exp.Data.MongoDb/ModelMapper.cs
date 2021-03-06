﻿using System;
using AutoMapper;
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

      CreateMap<ExpenseTypeMongoBaseDM, ExpenseTypeSM>()
        .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
        .ForMember(dst => dst.SyncDate, opt => opt.Ignore())
        .ForMember(dst => dst.SyncId, opt => opt.Ignore());


      CreateMap<ExpenseTypeSM, ExpenseTypeMongoDM>()
        .ForMember(dest => dest.Id, opt => opt.MapFrom(src => new ObjectId(src.Id)));

      CreateMap<ApplicationUserMongo, MongoUserDetails>();

      CreateMap<ExpenseSM, ExpenseMongoDM>()
        .ForMember(dest => dest.Id, opt => opt.MapFrom(src => new ObjectId(src.Id)))
        .ForMember(dest => dest.ExpenseValue, opt => opt.MapFrom(src => Convert.ToInt32(src.ExpenseValue * 100)));

      CreateMap<ExpenseTypeSM, ExpenseTypeMongoBaseDM>()
        .ForMember(dest => dest.Id, opt => opt.MapFrom(src => new ObjectId(src.Id)));

      CreateMap<UpdateExpenseSM, ExpenseMongoDM>()
        .ForMember(dest => dest.Id, opt => opt.MapFrom(src => new ObjectId(src.Id)))
        .ForMember(dest => dest.ExpenseValue, opt => opt.MapFrom(src => Convert.ToInt32(src.ExpenseValue * 100)));
      //.ForMember(d => d.ExpenseType, opt => opt.MapFrom(src => src.ExpenseType.ExpenseType));

      CreateMap<ExpenseMongoDM, ExpenseSM>()
        .ForMember(dest => dest.ExpenseValue, opt => opt.MapFrom(src => Convert.ToInt32(src.ExpenseValue / 100)))
        .ForMember(dst => dst.SyncDate, opt => opt.Ignore())
        .ForMember(dst => dst.SyncId, opt => opt.Ignore());

    }    
  }
}
