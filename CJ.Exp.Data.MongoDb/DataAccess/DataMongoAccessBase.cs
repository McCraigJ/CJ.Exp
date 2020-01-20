using CJ.Exp.BusinessLogic;
using CJ.Exp.Data.MongoDb.Interfaces;
using CJ.Exp.DomainInterfaces;
using MongoDB.Driver;

namespace CJ.Exp.Data.MongoDb.DataAccess
{
  public class DataMongoAccessBase
  {    
    protected MongoClient Client { get; }
    private IClientSessionHandle _mongoClientSession;
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
      if (_mongoClientSession != null)
      {
        throw new CjExpInvalidOperationException("Transaction Session already started");
      }
      _mongoClientSession = Client.StartSession();
      _mongoClientSession.StartTransaction();
    }

    public void CommitTransaction()
    {
      if (_mongoClientSession == null)
      {
        throw new CjExpInvalidOperationException("Transaction Session not started");
      }
      _mongoClientSession.CommitTransaction();
      _mongoClientSession = null;
    }

    public void RollbackTransaction()
    {
      _mongoClientSession?.AbortTransaction();
    }
  }
}
