using System;
using System.Collections.Generic;
using System.Text;
using CJ.Exp.DomainInterfaces;
using MongoDB.Driver;
using IAppMongoClient = CJ.Exp.Data.MongoDb.Interfaces.IAppMongoClient;

namespace CJ.Exp.Data.MongoDb.DataAccess
{
  public class DataMongoAccessBase<TCollection>
  {
    protected IMongoCollection<TCollection> Collection { get; }

    public DataMongoAccessBase(IAppMongoClient mongoClient, IApplicationSettings applicationSettings, string collectionName)
    {
      var client = mongoClient.GetClient();
      Collection = client.GetDatabase(applicationSettings.DatabaseName).GetCollection<TCollection>(collectionName);
    }
  }
}
