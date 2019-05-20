using System;
using System.Collections.Generic;
using System.Text;
using CJ.Exp.BusinessLogic;
using CJ.Exp.DomainInterfaces;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using MongoDB.Driver;
using IAppMongoClient = CJ.Exp.Data.MongoDb.Interfaces.IAppMongoClient;

namespace CJ.Exp.Data.MongoDb.DataAccess
{
  public class DataMongoAccessBase<TCollection>
  {
    protected IMongoCollection<TCollection> Collection { get; }
    protected MongoClient Client { get; }
    private IClientSessionHandle session;

    public DataMongoAccessBase(IAppMongoClient mongoClient, IApplicationSettings applicationSettings, string collectionName)
    {
      Client = mongoClient.GetClient();
      Collection = Client.GetDatabase(applicationSettings.DatabaseName).GetCollection<TCollection>(collectionName);
    }

    /// <summary>
    /// Start MongoDB Transaction. This should be used when more than one MongoDB operation is to be executed in one operation
    /// </summary>
    public void StartTransaction()
    {
      if (session != null)
      {
        throw new DomainException("Transaction session already started");
      }
      session = Client.StartSession();
    }

    public void CommitTransaction()
    {
      if (session == null)
      {
        throw new DomainException("Transaction session not started");
      }
      session.CommitTransaction();
      session = null;
    }
  }
}
