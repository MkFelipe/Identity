using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Identity.Domain.Entities;
using Identity.Domain.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace Identity.Domain.Services
{
	public class UserService : IUserService
	{
		public UserService() {}
		public bool CheckPass(string pass, string hash, string salt)
		{
			if (hash != GeneratePass(pass, salt))
            {
                return false;
            }
            return true;
		}

		public Password GeneratePass(string password)
		{
			var key = new byte[128];

            using (var randomNumber = RandomNumberGenerator.Create())
            {
                randomNumber.GetBytes(key);

                var secret = Convert.ToBase64String(key);

                return new Password { Hash = GeneratePass(password, secret), Salt = secret };
            }
		}

		public string GenerateToken(Guid id, string secret, string issuer, int expirationTimeinMin, string refresh, string nickname)
		{
			var tokenHandler = new JwtSecurityTokenHandler();
   
            var key = Encoding.ASCII.GetBytes(secret);

            DateTime exp = DateTime.UtcNow.AddMinutes(expirationTimeinMin);

            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("iss", issuer),
                    new Claim("eid", id.ToString()),
                    new Claim("jti", refresh),
                    new Claim("exp", exp.ToString()),
                    new Claim("nck", nickname)
                }),
                Issuer = issuer,
                Expires = exp,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
		}

		// PRIVATES
		private static string GeneratePass(string password, string secret)
        {
            using Rfc2898DeriveBytes pbkdf2 = new Rfc2898DeriveBytes(password, Encoding.UTF8.GetBytes(secret), 11023, HashAlgorithmName.SHA512);

            return Convert.ToBase64String(pbkdf2.GetBytes(512));
        }
	}

}
