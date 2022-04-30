using System;
using Identity.Domain.Entities.interfaces;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace Identity.Domain.Entities
{
	public class User: IEntity
	{
		[BsonId]
		[BsonElement("_id")]
		[JsonProperty("_id")]
		[BsonRepresentation(BsonType.ObjectId)]
		[JsonIgnore]
		public string Id { get; set; }
		public Guid EntityId { get; set; }
		public string Nickname { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Email { get; set; }
		public string Phone { get; set; }
		public bool IsDeleted { get; set; }
        public Password Password { get; set; }
		public DateTime CreatedAt { get; set; }
		public DateTime UpdatedAt { get; set; }
	}
}

