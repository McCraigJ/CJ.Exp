using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using CJ.Exp.Data.Interfaces;
using CJ.Exp.Data.MongoDb.DataModels;
using CJ.Exp.Data.MongoDb.Interfaces;
using CJ.Exp.DomainInterfaces;
using CJ.Exp.ServiceModels.AuthTokens;
using MongoDB.Driver;

namespace CJ.Exp.Data.MongoDb.DataAccess
{
  public class AuthTokenDataMongo : DataMongoAccessBase, IAuthTokensData
  {

    private readonly IMongoCollection<AuthTokenDM> _authTokenCollection;

    public AuthTokenDataMongo(IAppMongoClient mongoClient, IApplicationSettings applicationSettings) : base(mongoClient, applicationSettings)
    {
      _authTokenCollection = Database.GetCollection<AuthTokenDM>("authtokens");
    }

    public AuthTokenSM GetAuthToken(string token)
    {
      var authToken = _authTokenCollection.Find(x => x.Token == token).SingleOrDefault();
      return Mapper.Map<AuthTokenSM>(authToken);
    }

    public void AddAuthToken(AuthTokenSM authToken)
    {
      var authTokenDm = Mapper.Map<AuthTokenDM>(authToken);
      _authTokenCollection.InsertOne(authTokenDm);
    }

    public void DeleteAuthToken(string token)
    {
      _authTokenCollection.FindOneAndDelete<AuthTokenDM>(Builders<AuthTokenDM>.Filter.Eq("Token", token));
    }
  }
}
