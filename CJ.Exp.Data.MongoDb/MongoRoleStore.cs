using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using MongoDbGenericRepository;

namespace CJ.Exp.Data.MongoDb
{
  public class MongoRoleStore<TRole, TContext, TKey> : RoleStoreBase<TRole, TKey, IdentityUserRole<TKey>, IdentityRoleClaim<TKey>>    
    where TRole : IdentityRole<TKey>, new()    
    where TKey : IEquatable<TKey>
    where TContext : IMongoDbContext
  {

    private static TContext Context { get; set; }

    public MongoRoleStore(TContext context, IdentityErrorDescriber describer = null) : base(describer ?? new IdentityErrorDescriber())
    {
      if (context == null)
      {
        throw new ArgumentNullException(nameof(context));
      }

      Context = context;
    }

    public override Task<IdentityResult> CreateAsync(TRole role, CancellationToken cancellationToken = new CancellationToken())
    {
      throw new NotImplementedException();
    }

    public override Task<IdentityResult> UpdateAsync(TRole role, CancellationToken cancellationToken = new CancellationToken())
    {
      throw new NotImplementedException();
    }

    public override Task<IdentityResult> DeleteAsync(TRole role, CancellationToken cancellationToken = new CancellationToken())
    {
      throw new NotImplementedException();
    }

    public override Task<TRole> FindByIdAsync(string id, CancellationToken cancellationToken = new CancellationToken())
    {
      throw new NotImplementedException();
    }

    public override Task<TRole> FindByNameAsync(string normalizedName, CancellationToken cancellationToken = new CancellationToken())
    {
      throw new NotImplementedException();
    }

    public override Task<IList<Claim>> GetClaimsAsync(TRole role, CancellationToken cancellationToken = new CancellationToken())
    {
      throw new NotImplementedException();
    }

    public override Task AddClaimAsync(TRole role, Claim claim, CancellationToken cancellationToken = new CancellationToken())
    {
      throw new NotImplementedException();
    }

    public override Task RemoveClaimAsync(TRole role, Claim claim, CancellationToken cancellationToken = new CancellationToken())
    {
      throw new NotImplementedException();
    }

    public override IQueryable<TRole> Roles { get; }
  }
}
