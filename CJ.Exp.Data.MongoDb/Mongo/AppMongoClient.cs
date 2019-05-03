using CJ.Exp.DomainInterfaces;
using MongoDB.Driver;
using IMongoClient = CJ.Exp.Data.MongoDb.Interfaces.IMongoClient;

namespace CJ.Exp.Data.MongoDb.Mongo
{
  public class AppMongoClient : IMongoClient
  {
    public IApplicationSettings ApplicationSettings { get; }

    public AppMongoClient(IApplicationSettings applicationSettings)
    {
      ApplicationSettings = applicationSettings;
    }
    public MongoClient GetClient()
    {
      return new MongoClient($"{ApplicationSettings.ConnectionString}");
    }
  }
}
