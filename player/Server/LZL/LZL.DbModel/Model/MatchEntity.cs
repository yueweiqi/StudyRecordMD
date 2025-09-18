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
    public record MatchEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        /// <summary>
        /// 比赛名称
        /// </summary>
        [BsonElement("Name")]
        public string Name { get; set; }
        /// <summary>
        /// 蓝方
        /// </summary>
        [BsonRepresentation(BsonType.ObjectId)]
        public string BlueId { get; set; }

      
        public int BlueScore { get; set; }
        /// <summary>
        /// 红方
        /// </summary>
        [BsonRepresentation(BsonType.ObjectId)]
        public string RedId { get; set; }

       
        public int RedScore { get; set; }

        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public ProgressStateEnum State { get; set; }
    }
}
