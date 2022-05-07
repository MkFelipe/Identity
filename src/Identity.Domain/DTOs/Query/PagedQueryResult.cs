using System.Collections.Generic;
using Identity.Domain.Entities.interfaces;

namespace Identity.Domain.DTOs.Query
{
	public class PagedQueryResult<T> where T : IEntity
	{
		public List<T> Results { get; set; }
		public int CurrentPage { get; set; }
		public int PageSize { get; set; }
		public int TotalItems { get; set; }
	}
}
