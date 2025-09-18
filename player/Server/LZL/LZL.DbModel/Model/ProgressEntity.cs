using LZL.DbModel.Enums;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LZL.DbModel.Model
{
    public record ProgressEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public ProgressStateEnum State { get; set; }
        /// <summary>
        /// 当前比赛id
        /// </summary>
        [BsonRepresentation(BsonType.ObjectId)]
        public string MatchId { get; set; }
    }
}
