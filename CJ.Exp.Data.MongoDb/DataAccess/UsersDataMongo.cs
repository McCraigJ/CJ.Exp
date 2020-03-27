using AutoMapper;
using CJ.Exp.Data.Interfaces;
using CJ.Exp.Data.MongoDb.DataModels;
using CJ.Exp.DomainInterfaces;
using CJ.Exp.ServiceModels;
using CJ.Exp.ServiceModels.Users;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CJ.Exp.Data.MongoDb.Interfaces;

namespace CJ.Exp.Data.MongoDb.DataAccess
{
  public class UsersDataMongo : DataMongoAccessBase, IUsersData
  {
    private readonly IMongoCollection<ApplicationUserMongo> _collection;

    public UsersDataMongo(IAppMongoClient mongoClient, IApplicationSettings applicationSettings) : 
      base(mongoClient, applicationSettings)
    {
      _collection = Database.GetCollection<ApplicationUserMongo>("applicationUserMongos");
    }

    public async Task<List<UserSM>> GetUsersAsync()
    {      
      var users = await _collection.Find(_ => true).ToListAsync();
      return Mapper.Map<List<UserSM>>(users);
    }

    public async Task<GridResultSM<UserSM>> GetUsersAsync(UsersFilterSM filter)
    {
      if (filter?.GridFilter == null)
      {
        return null;
      }

      var query = _collection.Find(_ => true);

      var users = await query.Skip(filter.GridFilter.Skip).Limit(filter.GridFilter.ItemsPerPage).ToListAsync();

      var count = await _collection.CountDocumentsAsync(x => true);

      return new GridResultSM<UserSM>(filter?.GridFilter?.PageNumber ?? 0,
        (int)count,
        filter?.GridFilter?.ItemsPerPage ?? 0,
        null, null,
        Mapper.Map<List<UserSM>>(users));
    }

    public async Task<UserSM> GetUserByIdAsync(string id)
    {
      var user = await _collection.Find(x => x.Id == new Guid(id)).SingleOrDefaultAsync();
      return Mapper.Map<UserSM>(user);
    }
  }
}
