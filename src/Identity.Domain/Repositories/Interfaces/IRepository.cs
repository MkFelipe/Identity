using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Identity.Domain.DTOs.Query;
using Identity.Domain.Entities.interfaces;
using MongoDB.Driver;

namespace Identity.Domain.Repositories.Interfaces
{
	public interface IRepository<T> where T : IEntity
	{
		/// <summary>
		/// Insert a new item to the collection
		/// </summary>
		/// <param name="item"></param>
		/// <returns>The item inserted</returns>
		Task InsertAsync(T item);
		/// <summary>
		/// Update an intem in the collection.
		/// </summary>
		/// <param name="item"></param>
		/// <returns></returns>
		Task UpdateAsync(Guid entityId, UpdateDefinition<T> updateDefinition);
		/// <summary>
		/// Find an item at the collection based in an expression
		/// </summary>
		/// <param name="expression">Boolean expression related to the search query.</param>
		/// <returns>An objec on the collection or null.</returns>
		Task<T> FindOneAsync(Expression<Func<T, bool>> expression);
		/// <summary>
		/// Find items at the collection based in an expression
		/// </summary>
		/// <param name="expression">Boolean expression related to the search query.</param>
		/// <returns>An objec on the collection or null.</returns>
		Task<List<T>> FindAsync(Expression<Func<T, bool>> expression);
		/// <summary>
		/// Get paged results based on the search parameters.
		/// </summary>
		/// <param name="expression">Boolean expression related to the search query.</param>
		/// <param name="current">Search current page.</param>
		/// <param name="pageSize">Search page size.</param>
		/// <returns>PagedQueryResult object.</returns>
		Task<PagedQueryResult<T>> GetPaged(Expression<Func<T, bool>> expression, int current, int pageSize);
		/// <summary>
		/// Find all items from a collection.
		/// </summary>
		/// <returns>A list of T or null.</returns>
		Task<List<T>> FindAllAsync();
		/// <summary>
		/// Sets the T alement as deleted.
		/// </summary>
		void Delete(Guid entityId);
	}

}
