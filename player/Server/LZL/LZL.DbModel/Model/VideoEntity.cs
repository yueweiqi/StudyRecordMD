using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LZL.DbModel.Enums;

namespace LZL.DbModel.Model
{
    public record VideoEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public DateTime? StartTime { get; set; }

        public string? VideoUrl { get; set; }

        public ProgressStateEnum State { get; set; }
    }
}
