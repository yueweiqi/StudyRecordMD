using LZL.DbModel.Enums;
using LZL.DbModel.Model;
using LZL.DbModel.ModelDto.TeamEntityDto;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LZL.DbModel.ModelDto.MatchEntityDto
{
    public record PageDataMatchEntityDto
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        /// <summary>
        /// 比赛名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 蓝方
        /// </summary>
        [BsonRepresentation(BsonType.ObjectId)]
        public string BlueId { get; set; }
        public List<DataTeamEntityDto>? BlueTeam { get; set; } = new();
        public int BlueScore { get; set; }
        /// <summary>
        /// 红方
        /// </summary>
        [BsonRepresentation(BsonType.ObjectId)]
        public string RedId { get; set; }
        public List<DataTeamEntityDto>? RedTeam { get; set; } = new();
        public int RedScore { get; set; }

        public DateTime? StartTime { get; set; }
        public string? StartTimeStr
        {
            get
            {
                if (StartTime == null)
                    return "";
                return StartTime.Value.ToString("yyyy-MM-dd hh:mm:ss");
            }
        }
        public DateTime? EndTime { get; set; }
        public string? EndTimeStr
        {
            get
            {
                if (EndTime == null)
                    return "";
                return EndTime.Value.ToString("yyyy-MM-dd hh:mm:ss");
            }
        }
        public ProgressStateEnum State { get; set; }
    }
}
