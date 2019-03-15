using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using MongoDbGenericRepository.Models;
using MongoDB.Driver;

namespace CJ.Exp.Data.MongoDb.User
{
  public class MongoDbIdentityUser : MongoIdentityUser<string>
  {
    public MongoDbIdentityUser() { }

    public MongoDbIdentityUser(string userName) : base(userName) { }

    public MongoDbIdentityUser(string userName, string email) : base(userName, email) { }    
  }

  public class MongoIdentityUser<TKey> : IdentityUser<TKey>, IDocument<TKey>
    where TKey : IEquatable<TKey>
  {
    public int Version { get; set; }

    public DateTime CreatedOn { get; private set; }

//    public List<TKey> Roles { get; set; }

//    public List<UserLoginInfo> Logins { get; set; }
  
      

  }

}
