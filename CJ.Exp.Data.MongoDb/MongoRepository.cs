//using System;
//using System.Collections.Generic;
//using System.Text;
//using MongoDbGenericRepository;
//using MongoDB.Driver;

//namespace CJ.Exp.Data.MongoDb
//{
//  public interface IMongoRepository : IBaseMongoRepository
//  {
//    /// <summary>
//    /// Drops a collections.
//    /// </summary>
//    /// <typeparam name="TDocument">The type of the document used to define the collection name.</typeparam>
//    void DropCollection<TDocument>();

//    /// <summary>
//    /// Drops a partitioned collection.
//    /// </summary>
//    /// <typeparam name="TDocument">The type of the document used to define the collection name.</typeparam>
//    /// <param name="partitionKey">The partition key of the collection.</param>
//    void DropCollection<TDocument>(string partitionKey);

//    /// <summary>
//    /// The MongoDb context.
//    /// </summary>
//    IMongoDbContext Context { get; }
//  }

//  public class MongoRepository : BaseMongoRepository, IMongoRepository
//  {
//    public MongoRepository(string connectionString, string databaseName) : base(connectionString, databaseName)
//    {
//    }

//    public MongoRepository(IMongoDbContext mongoDbContext) : base(mongoDbContext)
//    {
//    }    

//    public void DropCollection<TDocument>()
//    {
//      MongoDbContext.DropCollection<TDocument>();
//    }

//    public void DropCollection<TDocument>(string partitionKey)
//    {
//      MongoDbContext.DropCollection<TDocument>(partitionKey);
//    }

//    public IMongoDbContext Context => MongoDbContext;
//  }
//}
