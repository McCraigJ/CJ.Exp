using AutoMapper;
using CJ.Exp.Data.Interfaces;
using CJ.Exp.Data.MongoDb.User;
using CJ.Exp.DomainInterfaces;
using CJ.Exp.ServiceModels.Users;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using IMongoClient = CJ.Exp.Data.MongoDb.Interfaces.IMongoClient;

namespace CJ.Exp.Data.MongoDb.DataAccess
{
  public class UsersDataMongo : IUsersData
  {
    private IMongoCollection<ApplicationUserMongo> Collection { get; }    

    public UsersDataMongo(IMongoClient mongoClient, IApplicationSettings applicationSettings)
    {
      var client = mongoClient.GetClient();
      Collection = client.GetDatabase(applicationSettings.DatabaseName).GetCollection<ApplicationUserMongo>("applicationUserMongos");
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
