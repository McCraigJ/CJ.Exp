using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Driver;

namespace CJ.Exp.Data.MongoDb.Interfaces
{
  public interface IMongoClient
  {
    MongoClient GetClient();
  }
}
