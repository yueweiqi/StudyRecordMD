using LZL.DbModel.Enums;
using LZL.DbModel.Extension;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LZL.DbModel.Model
{
    public record MatchPlayerCommentEntity
    {
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        [BsonId]
        public string Id { get; set; }

        public string? Name { get; set; }
        public MatchCommentInfoDto Match { get; set; }

        public TeamCommentInfoDto BlueTeam { get; set; }

        public List<PlayerCommentInfoDto> BluePlayerList { get; set; } = new();

        public TeamCommentInfoDto RedTeam { get; set; }
        public List<PlayerCommentInfoDto> RedPlayerList { get; set; } = new();

        public ProgressStateEnum State { get; set; } = ProgressStateEnum.None;
    }

    public record MatchCommentInfoDto 
    {
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string? Id { get; set; }
        public string? Name { get; set; }
        public DateTime? StartTime { get; set; }
        public string? StartTimeStr
        {
            get
            {
                if (StartTime == null)
                    return "";
                TimeZoneInfo localZone = TimeZoneInfo.Local;
                DateTime localTime = TimeZoneInfo.ConvertTime(StartTime.Value, localZone);
                return localTime.ToString("yyyy-MM-dd HH:mm:ss");
            }
        }
        public ProgressStateEnum State { get; set; }
    }

    public record TeamCommentInfoDto 
    {
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string? Id { get; set; }
        public string? Name { get; set; }
        public string? Avatar { get; set; }
    }
    public record PlayerCommentInfoDto 
    {
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }
        public string? Name { get; set; }

        public PositionEnum Position { get; set; }

        public string? PositionStr { get { return  Position.GetDescription(); } }

        public RankNameEnum RankName { get; set; }

        public string? School { get; set; }
        public string? CommentInfo { get; set; }
    }
}
