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
    private readonly IMongoCollection<RefreshTokenDM> _refreshTokenCollection;

    public AuthTokenDataMongo(IAppMongoClient mongoClient, IApplicationSettings applicationSettings) : base(mongoClient, applicationSettings)
    {
      _authTokenCollection = Database.GetCollection<AuthTokenDM>("authtokens");
      _refreshTokenCollection = Database.GetCollection<RefreshTokenDM>("refreshtokens");
    }

    public AuthTokenSM GetAuthToken(string token)
    {
      var authToken = _authTokenCollection.Find(x => x.Token == token).SingleOrDefault();
      return Mapper.Map<AuthTokenSM>(authToken);
    }

    public void AddAuthToken(AuthTokenSM authToken)
    {
      var tokenCollectionIndexBuilder = Builders<AuthTokenDM>.IndexKeys;
      var indexModel = new CreateIndexModel<AuthTokenDM>(tokenCollectionIndexBuilder.Text(x => x.Token));
      _authTokenCollection.Indexes.CreateOne(indexModel); //.ConfigureAwait(false);

      var authTokenDm = Mapper.Map<AuthTokenDM>(authToken);
      _authTokenCollection.InsertOne(authTokenDm);
    }

    public void DeleteAuthToken(string token)
    {
      _authTokenCollection.FindOneAndDelete<AuthTokenDM>(Builders<AuthTokenDM>.Filter.Eq("Token", token));
    }

    public RefreshTokenSM GetRefreshTokenForUserId(string userId)
    {
      var refreshToken = _refreshTokenCollection.Find(x => x.UserId == new Guid(userId)).SingleOrDefault();
      return Mapper.Map<RefreshTokenSM>(refreshToken);
    }

    public void AddRefreshToken(RefreshTokenSM refreshToken)
    {
      var tokenCollectionIndexBuilder = Builders<RefreshTokenDM>.IndexKeys;
      var indexModel = new CreateIndexModel<RefreshTokenDM>(tokenCollectionIndexBuilder.Text(x => x.UserId), new CreateIndexOptions { Unique = true});
      _refreshTokenCollection.Indexes.CreateOne(indexModel); //.ConfigureAwait(false);

      var refreshTokenDm = Mapper.Map<RefreshTokenDM>(refreshToken);
      _refreshTokenCollection.InsertOne(refreshTokenDm);
    }

    public void DeleteRefreshTokenForUser(string userId)
    {
      _refreshTokenCollection.FindOneAndDelete<RefreshTokenDM>(Builders<RefreshTokenDM>.Filter.Eq("UserId", new Guid(userId)));
    }
  }
}
