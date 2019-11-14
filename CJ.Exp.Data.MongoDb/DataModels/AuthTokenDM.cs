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

  public class RefreshTokenDM
  {
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string RefreshToken { get; set; }
    public DateTime Expiry { get; set; }
  }
}
