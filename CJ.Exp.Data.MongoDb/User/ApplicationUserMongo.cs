using AspNetCore.Identity.MongoDbCore.Models;
using CJ.Exp.DomainInterfaces;
using System;

namespace CJ.Exp.Data.MongoDb.User
{
  public class ApplicationUserMongo : MongoIdentityUser<Guid>, IApplicationUser
  {
    public ApplicationUserMongo() : base()
    {
    }

    public ApplicationUserMongo(string userName, string email) : base(userName, email)
    {
    }

    public string ApplicationId
    {
      get => Id.ToString();
      set => Id = Guid.Parse(value);
    }

    public string FirstName { get; set; }
    public string LastName { get; set; }
  }

  public class ApplicationRoleMongo : MongoIdentityRole<Guid>, IApplicationRole
  {
    public ApplicationRoleMongo() : base()
    {
    }

    public ApplicationRoleMongo(string roleName) : base(roleName)
    {
    }
  }
}
