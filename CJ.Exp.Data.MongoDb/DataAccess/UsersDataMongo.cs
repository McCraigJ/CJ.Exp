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
  public class UsersDataMongo : DataMongoAccessBase, IUsersData
  {
    private readonly IMongoCollection<ApplicationUserMongo> _collection;

    public UsersDataMongo(IAppMongoClient mongoClient, IApplicationSettings applicationSettings) : 
      base(mongoClient, applicationSettings)
    {
      _collection = Database.GetCollection<ApplicationUserMongo>("applicationUserMongos");
    }

    public List<UserSM> GetUsers()
    {      
      var users = _collection.Find(_ => true).ToList();
      return Mapper.Map<List<UserSM>>(users);
    }

    public UserSM GetUserById(string id)
    {
      var user = _collection.Find(x => x.Id == new Guid(id)).SingleOrDefault();
      return Mapper.Map<UserSM>(user);
    }

    //public IQueryable<string> GetCurrentUserRoles(string userId)
    //{
    //  throw new NotImplementedException();
    //}
  }
}
