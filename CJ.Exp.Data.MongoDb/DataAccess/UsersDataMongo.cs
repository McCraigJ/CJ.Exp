using AutoMapper;
using CJ.Exp.Data.Interfaces;
using CJ.Exp.DomainInterfaces;
using CJ.Exp.ServiceModels.Users;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using CJ.Exp.Data.MongoDb.DataModels;
using IAppMongoClient = CJ.Exp.Data.MongoDb.Interfaces.IAppMongoClient;

namespace CJ.Exp.Data.MongoDb.DataAccess
{
  public class UsersDataMongo : DataMongoAccessBase<ApplicationUserMongo>, IUsersData
  {    
    public UsersDataMongo(IAppMongoClient mongoClient, IApplicationSettings applicationSettings) : 
      base(mongoClient, applicationSettings, "applicationUserMongos")
    {      
    }

    public List<UserSM> GetUsers()
    {      
      var users = Collection.Find(_ => true).ToList();
      return Mapper.Map<List<UserSM>>(users);
    }

    public UserSM GetUserById(string id)
    {
      var user = Collection.Find(x => x.Id == new Guid(id)).SingleOrDefault();
      return Mapper.Map<UserSM>(user);
    }

    //public IQueryable<string> GetCurrentUserRoles(string userId)
    //{
    //  throw new NotImplementedException();
    //}
  }
}
