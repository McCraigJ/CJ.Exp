using AutoMapper;
using CJ.Exp.Data.Interfaces;
using CJ.Exp.Data.MongoDb.DataModels;
using CJ.Exp.Data.MongoDb.Interfaces;
using CJ.Exp.DomainInterfaces;
using CJ.Exp.ServiceModels.AuthTokens;
using MongoDB.Driver;
using System;
using System.Threading.Tasks;

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

      var indexBuilder = Builders<AuthTokenDM>.IndexKeys;
      var tokenIndexModel = new CreateIndexModel<AuthTokenDM>(indexBuilder.Text(x => x.Token));
      _authTokenCollection.Indexes.CreateOne(tokenIndexModel);

      var refreshTokenIndexBuilder = Builders<RefreshTokenDM>.IndexKeys;
      var userIdIndexModel = new CreateIndexModel<RefreshTokenDM>(refreshTokenIndexBuilder.Text(x => x.UserId), new CreateIndexOptions { Unique = true });
      _refreshTokenCollection.Indexes.CreateOne(userIdIndexModel);

    }

    public async Task<AuthTokenSM> GetAuthTokenAsync(string token)
    {
      var authToken = await _authTokenCollection.Find(x => x.Token == token).SingleOrDefaultAsync();
      return Mapper.Map<AuthTokenSM>(authToken);
    }

    public async Task AddAuthTokenAsync(AuthTokenSM authToken)
    {
      var authTokenDm = Mapper.Map<AuthTokenDM>(authToken);
      await _authTokenCollection.InsertOneAsync(authTokenDm);
    }

    public async Task DeleteAuthTokenAsync(string token)
    {
      await _authTokenCollection.FindOneAndDeleteAsync<AuthTokenDM>(Builders<AuthTokenDM>.Filter.Eq("Token", token));
    }

    public async Task<RefreshTokenSM> GetRefreshTokenForUserIdAsync(string userId)
    {
      var refreshToken = await _refreshTokenCollection.Find(x => x.UserId == new Guid(userId)).SingleOrDefaultAsync();
      return Mapper.Map<RefreshTokenSM>(refreshToken);
    }

    public async Task AddRefreshTokenAsync(RefreshTokenSM refreshToken)
    {
      var refreshTokenDm = Mapper.Map<RefreshTokenDM>(refreshToken);
      await _refreshTokenCollection.InsertOneAsync(refreshTokenDm);
    }

    public async Task DeleteRefreshTokenForUserAsync(string userId)
    {
      await _refreshTokenCollection.FindOneAndDeleteAsync<RefreshTokenDM>(Builders<RefreshTokenDM>.Filter.Eq("UserId", new Guid(userId)));
    }
  }
}
