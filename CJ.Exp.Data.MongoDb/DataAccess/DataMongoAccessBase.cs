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
  public class DataMongoAccessBase
  {    
    protected MongoClient Client { get; }
    private IClientSessionHandle session;
    protected IMongoDatabase Database { get; }

    public DataMongoAccessBase(IAppMongoClient mongoClient, IApplicationSettings applicationSettings)
    {
      Client = mongoClient.GetClient();
      Database = Client.GetDatabase(applicationSettings.DatabaseName);
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
      session.StartTransaction();
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
