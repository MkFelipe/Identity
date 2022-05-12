using System.Threading.Tasks;
using Identity.Domain.Entities;
using Identity.Domain.Repositories.Interfaces;
using MongoDB.Driver;

namespace Identity.Domain.Repositories
{
	public class UserRepository : Repository<User>, IUserRepository
	{
		public UserRepository(IMongoClient client) : base(client)
		{
			_collection = _database.GetCollection<User>("Users");
		}

		public async Task<User> GetUserByNickname(string nickname)
		{
			var user = await FindOneAsync(x => x.Nickname == nickname);

			return user;
		}
	}
}

