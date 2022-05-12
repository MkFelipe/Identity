using System.Threading.Tasks;
using Identity.Domain.Entities;

namespace Identity.Domain.Repositories.Interfaces
{
	public interface IUserRepository : IRepository<User>
	{
		/// <summary>
		/// Find an user by nickname.
		/// </summary>
		/// <param name="nickname">string to be searched.</param>
		/// <returns>A user or null</returns>
		Task<User> GetUserByNickname(string nickname);
	}
}

