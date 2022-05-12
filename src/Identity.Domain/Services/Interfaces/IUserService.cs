using System;
using Identity.Domain.Entities;

namespace Identity.Domain.Services.Interfaces
{
	public interface IUserService
	{
		public bool CheckPass(string pass, string hash, string salt);
		public Password GeneratePass(string password);
		public string GenerateToken(Guid id, string secret, string issuer, int expirationTimeinMin, string refresh, string nickname);
	}

}
