//using Microsoft.AspNetCore.Identity;
//using MongoDbGenericRepository;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Security.Claims;
//using System.Threading;
//using System.Threading.Tasks;

//namespace CJ.Exp.Data.MongoDb
//{
//  public class MongoUserStore<TUser, TRole, TContext, TKey> : 
//    UserStoreBase<TUser, TRole, TKey, IdentityUserClaim<TKey>, IdentityUserRole<TKey>, IdentityUserLogin<TKey>, IdentityUserToken<TKey>, IdentityRoleClaim<TKey>>
//    where TUser : IdentityUser<TKey>, new()
//    where TRole : IdentityRole<TKey>, new()
//    where TContext : IMongoDbContext
//    where TKey : IEquatable<TKey>
//  {

//    private static TContext Context { get; set; }

//    public MongoUserStore(TContext context, IdentityErrorDescriber describer = null) : base(describer ?? new IdentityErrorDescriber())
//    {
//      if (context == null)
//      {
//        throw new ArgumentNullException(nameof(context));
//      }

//      Context = context;
//    }

//    public override Task<IdentityResult> CreateAsync(TUser user, CancellationToken cancellationToken = new CancellationToken())
//    {
//      throw new NotImplementedException();
//    }

//    public override Task<IdentityResult> UpdateAsync(TUser user, CancellationToken cancellationToken = new CancellationToken())
//    {
//      throw new NotImplementedException();
//    }

//    public override Task<IdentityResult> DeleteAsync(TUser user, CancellationToken cancellationToken = new CancellationToken())
//    {
//      throw new NotImplementedException();
//    }

//    public override Task<TUser> FindByIdAsync(string userId, CancellationToken cancellationToken = new CancellationToken())
//    {
//      throw new NotImplementedException();
//    }

//    public override Task<TUser> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken = new CancellationToken())
//    {
//      throw new NotImplementedException();
//    }

//    protected override Task<TUser> FindUserAsync(TKey userId, CancellationToken cancellationToken)
//    {
//      throw new NotImplementedException();
//    }

//    protected override Task<IdentityUserLogin<TKey>> FindUserLoginAsync(TKey userId, string loginProvider, string providerKey, CancellationToken cancellationToken)
//    {
//      throw new NotImplementedException();
//    }

//    protected override Task<IdentityUserLogin<TKey>> FindUserLoginAsync(string loginProvider, string providerKey, CancellationToken cancellationToken)
//    {
//      throw new NotImplementedException();
//    }

//    public override Task<IList<Claim>> GetClaimsAsync(TUser user, CancellationToken cancellationToken = new CancellationToken())
//    {
//      throw new NotImplementedException();
//    }

//    public override Task AddClaimsAsync(TUser user, IEnumerable<Claim> claims, CancellationToken cancellationToken = new CancellationToken())
//    {
//      throw new NotImplementedException();
//    }

//    public override Task ReplaceClaimAsync(TUser user, Claim claim, Claim newClaim, CancellationToken cancellationToken = new CancellationToken())
//    {
//      throw new NotImplementedException();
//    }

//    public override Task RemoveClaimsAsync(TUser user, IEnumerable<Claim> claims, CancellationToken cancellationToken = new CancellationToken())
//    {
//      throw new NotImplementedException();
//    }

//    public override Task<IList<TUser>> GetUsersForClaimAsync(Claim claim, CancellationToken cancellationToken = new CancellationToken())
//    {
//      throw new NotImplementedException();
//    }

//    protected override Task<IdentityUserToken<TKey>> FindTokenAsync(TUser user, string loginProvider, string name, CancellationToken cancellationToken)
//    {
//      throw new NotImplementedException();
//    }

//    protected override Task AddUserTokenAsync(IdentityUserToken<TKey> token)
//    {
//      throw new NotImplementedException();
//    }

//    protected override Task RemoveUserTokenAsync(IdentityUserToken<TKey> token)
//    {
//      throw new NotImplementedException();
//    }

//    public override IQueryable<TUser> Users { get; }

//    public override Task AddLoginAsync(TUser user, UserLoginInfo login, CancellationToken cancellationToken = new CancellationToken())
//    {
//      throw new NotImplementedException();
//    }

//    public override Task RemoveLoginAsync(TUser user, string loginProvider, string providerKey, CancellationToken cancellationToken = new CancellationToken())
//    {
//      throw new NotImplementedException();
//    }

//    public override Task<IList<UserLoginInfo>> GetLoginsAsync(TUser user, CancellationToken cancellationToken = new CancellationToken())
//    {
//      throw new NotImplementedException();
//    }

//    public override Task<TUser> FindByEmailAsync(string normalizedEmail, CancellationToken cancellationToken = new CancellationToken())
//    {
//      throw new NotImplementedException();
//    }

//    public override Task<bool> IsInRoleAsync(TUser user, string normalizedRoleName, CancellationToken cancellationToken = new CancellationToken())
//    {
//      throw new NotImplementedException();
//    }

//    protected override Task<TRole> FindRoleAsync(string normalizedRoleName, CancellationToken cancellationToken)
//    {
//      throw new NotImplementedException();
//    }

//    protected override Task<IdentityUserRole<TKey>> FindUserRoleAsync(TKey userId, TKey roleId, CancellationToken cancellationToken)
//    {
//      throw new NotImplementedException();
//    }

//    public override Task<IList<TUser>> GetUsersInRoleAsync(string normalizedRoleName, CancellationToken cancellationToken = new CancellationToken())
//    {
//      throw new NotImplementedException();
//    }

//    public override Task AddToRoleAsync(TUser user, string normalizedRoleName, CancellationToken cancellationToken = new CancellationToken())
//    {
//      throw new NotImplementedException();
//    }

//    public override Task RemoveFromRoleAsync(TUser user, string normalizedRoleName, CancellationToken cancellationToken = new CancellationToken())
//    {
//      throw new NotImplementedException();
//    }

//    public override Task<IList<string>> GetRolesAsync(TUser user, CancellationToken cancellationToken = new CancellationToken())
//    {
//      throw new NotImplementedException();
//    }
//  }
//}
