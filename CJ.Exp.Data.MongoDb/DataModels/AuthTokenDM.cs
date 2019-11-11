using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson;

namespace CJ.Exp.Data.MongoDb.DataModels
{
  public class AuthTokenDM
  {
    public ObjectId Id { get; set; }
    public string Token { get; set; }
    public DateTime Issued { get; set; }
    public DateTime Expiry { get; set; }
  }
}
