using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;

namespace Csharp_tictoe_game.GameModels.Base
{
    public class BaseModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }


        public DateTime CreatedAt { get; set; }
        public DateTime UpdateAt { get; set; }

        public BaseModel()
        {
            CreatedAt = DateTime.Now;
        }
    }
}
