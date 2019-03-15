using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using MongoDbGenericRepository;
using System;
using System.Reflection;

namespace CJ.Exp.Data.MongoDb
{
  public static class MongoDbIdentityBuilderExtensions
  {
    //public static IdentityBuilder AddMongoDbStores<TContext>(this IdentityBuilder builder, IMongoDbContext mongoDbContext) where TContext : IMongoDbContext
    //{
    //  if (mongoDbContext == null)
    //  {
    //    throw new ArgumentNullException(nameof(mongoDbContext));
    //  }

    //  builder.Services.TryAddSingleton<IMongoDbContext>(mongoDbContext);
    //  builder.Services.TryAddSingleton<IMongoRepository>(new MongoRepository(mongoDbContext));

    //  AddStores(builder.Services, builder.UserType, builder.RoleType, typeof(TContext));
    //  return builder;
    //}

    public static IdentityBuilder AddMongoDbStores<TUser, TRole, TKey>(this IdentityBuilder builder, string connectionString, string databaseName)
      where TUser : IdentityUser<TKey>, new()
      where TRole : IdentityRole<TKey>, new()
      where TKey : IEquatable<TKey>

    {
      if (string.IsNullOrEmpty(connectionString))
      {
        throw new ArgumentNullException(nameof(connectionString));
      }

      if (string.IsNullOrEmpty(databaseName))
      {
        throw new ArgumentNullException(nameof(databaseName));
      }

      builder.Services.TryAddSingleton<MongoDbSettings>(new MongoDbSettings
      {
        ConnectionString = connectionString,
        DatabaseName = databaseName
      });
      builder.AddMongoDbStores<TUser, TRole, TKey>(new MongoDbContext(connectionString, databaseName));
      return builder;
    }

    public static IdentityBuilder AddMongoDbStores<TUser, TRole, TKey>(this IdentityBuilder builder, IMongoDbContext mongoDbContext)
      where TUser : IdentityUser<TKey>, new()
      where TRole : IdentityRole<TKey>, new()
      where TKey : IEquatable<TKey>
    {
      if (mongoDbContext == null)
      {
        throw new ArgumentNullException(nameof(mongoDbContext));
      }
      builder.Services.TryAddSingleton<IMongoDbContext>(mongoDbContext);
      builder.Services.TryAddSingleton<IMongoRepository>(new MongoRepository(mongoDbContext));
      builder.Services.TryAddScoped<IUserStore<TUser>>(provider =>
      {
        return new MongoUserStore<TUser, TRole, IMongoDbContext, TKey>(provider.GetService<IMongoDbContext>());
      });

      builder.Services.TryAddScoped<IRoleStore<TRole>>(provider =>
      {
        return new MongoRoleStore<TRole, IMongoDbContext, TKey>(provider.GetService<IMongoDbContext>());
      });
      return builder;
    }

    //public static IdentityBuilder AddMongoDbStores<TUser, TRole, TKey>(this IdentityBuilder builder, IMongoDbContext mongoDbContext)
    //  where TUser : IdentityUser<TKey>, new()
    //  where TRole : IdentityRole<TKey>, new()
    //  where TKey : IEquatable<TKey>
    //{
    //  if (mongoDbContext == null)
    //  {
    //    throw new ArgumentNullException(nameof(mongoDbContext));
    //  }

    //  builder.Services.TryAddSingleton<IMongoDbContext>(mongoDbContext);
    //  builder.Services.TryAddSingleton<IMongoRepository>(new MongoRepository(mongoDbContext));
    //  builder.Services.TryAddScoped<IUserStore<TUser>>(provider =>
    //  {
    //    return new MongoUserStore<TUser, TRole, IMongoDbContext, TKey>(provider.GetService<IMongoDbContext>());
    //  });

    //  builder.Services.TryAddScoped<IRoleStore<TRole>>(provider =>
    //  {
    //    return new MongoRoleStore<TRole, IMongoDbContext, TKey>(provider.GetService<IMongoDbContext>());
    //  });
    //  return builder;
    //}

    //private static void AddStores(IServiceCollection services, Type userType, Type roleType, Type contextType)
    //{
    //  var identityUserType = FindGenericBaseType(userType, typeof(IdentityUser));
    //  if (identityUserType == null)
    //  {
    //    throw new InvalidOperationException("Developer Error: Specified user type doesn't inherit from IdentityUser");
    //  }

    //  var keyType = identityUserType.GenericTypeArguments[0];

    //  if (roleType != null)
    //  {
    //    var identityRoleType = FindGenericBaseType(roleType, typeof(IdentityRole));
    //    if (identityRoleType == null)
    //    {
    //      throw new InvalidOperationException("Developer Error: Specified role type doesn't inherit from IdentityRole");
    //    }

    //    Type userStoreType = null;
    //    Type roleStoreType = null;

    //    // If its a custom DbContext, we can only add the default POCOs
    //    userStoreType = typeof(MongoUserStore<,,,>).MakeGenericType(userType, roleType, contextType, keyType);
    //    roleStoreType = typeof(MongoRoleStore<,>).MakeGenericType(roleType, contextType, keyType);

    //    services.TryAddScoped(typeof(IUserStore<>).MakeGenericType(userType), userStoreType);
    //    services.TryAddScoped(typeof(IRoleStore<>).MakeGenericType(roleType), roleStoreType);
    //  }
    //  else
    //  {   // No Roles
    //    Type userStoreType = null;
    //    // If its a custom DbContext, we can only add the default POCOs
    //    userStoreType = typeof(MongoUserStore<,,,>).MakeGenericType(userType, roleType, contextType, keyType);
    //    services.TryAddScoped(typeof(IUserStore<>).MakeGenericType(userType), userStoreType);
    //  }

    //}

    //private static TypeInfo FindGenericBaseType(Type currentType, Type genericBaseType)
    //{
    //  var type = currentType;
    //  while (type != null)
    //  {
    //    var typeInfo = type.GetTypeInfo();
    //    var genericType = type.IsGenericType ? type.GetGenericTypeDefinition() : null;
    //    if (genericType != null && genericType == genericBaseType)
    //    {
    //      return typeInfo;
    //    }
    //    type = type.BaseType;
    //  }
    //  return null;
    //}
  }

}
