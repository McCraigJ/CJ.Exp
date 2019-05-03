using CJ.Exp.DomainInterfaces;
using MongoDB.Driver;
using IAppMongoClient = CJ.Exp.Data.MongoDb.Interfaces.IAppMongoClient;

namespace CJ.Exp.Data.MongoDb.Mongo
{
  public class AppMongoClient : IAppMongoClient
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
