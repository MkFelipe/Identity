using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Identity.Domain.DTOs.Query;
using Identity.Domain.Entities.interfaces;
using Identity.Domain.Repositories.Interfaces;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Identity.Domain.Repositories
{
	public class Repository<T> : IRepository<T> where T : IEntity
	{
		internal IMongoCollection<T> _collection;
		internal IMongoDatabase _database;
		public Repository(IMongoClient client)
		{
			var camelCaseConvention = new ConventionPack { new CamelCaseElementNameConvention() };
			ConventionRegistry.Register("CamelCase", camelCaseConvention, type => true);

			_database = client.GetDatabase("Identity");
		}

		public async void Delete(Guid entityId)
		{
			var update = Builders<T>.Update
				 .Set(x => x.IsDeleted, true);

			await UpdateAsync(entityId, update);
		}

		public async Task<List<T>> FindAllAsync()
		{
			return await _collection.AsQueryable().ToListAsync();
		}

		public async Task<T> FindOneAsync(Expression<Func<T, bool>> expression)
		{
			return await _collection.AsQueryable().FirstOrDefaultAsync(expression);
		}

		public async Task<List<T>> FindAsync(Expression<Func<T, bool>> expression)
		{
			var cursor = _collection.AsQueryable().Where(expression);

			return await cursor.ToListAsync();
		}

		public async Task<PagedQueryResult<T>> GetPaged(Expression<Func<T, bool>> expression, int current, int pageSize)
		{
			var cursor = _collection.AsQueryable().Where(expression);

			var result = new PagedQueryResult<T>
			{
				CurrentPage = current,
				PageSize = pageSize,
				TotalItems = await cursor.CountAsync(),
				Results = await cursor.Skip((current - 1) * pageSize).Take(pageSize).ToListAsync()
			};

			return result;
		}

		public async Task InsertAsync(T item)
		{
			await _collection.InsertOneAsync(item);
		}

		public async Task UpdateAsync(Guid entityId, UpdateDefinition<T> updateDefinition)
		{
			var filter = new BsonDocument("entityId", new BsonBinaryData(entityId, GuidRepresentation.CSharpLegacy));
			await _collection.FindOneAndUpdateAsync(filter, updateDefinition);
		}
	}
}