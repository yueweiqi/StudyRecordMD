using LZL.DbModel.Enums;
using LZL.DbModel.Extension;
using LZL.DbModel.Model;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LZL.DbModel.ModelDto.MatchPlayerCommentEntityDto
{
    public record DataCurrentMatchPlayerEntityDto
    {
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        [BsonId]
        public string Id { get; set; }
        public string? Name { get; set; }
        public List<TeamPlayerCommentInfoDto> PlayerList { get; set; } = new();
    }
    public record TeamPlayerCommentInfoDto 
    {
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }
        public string? Name { get; set; }

        public PositionEnum Position { get; set; }

        public string? PositionStr { get { return Position.GetDescription(); } }

        public RankNameEnum RankName { get; set; }

        public string? School { get; set; }
        public string? CommentInfo { get; set; }
        public string? TeamName { get; set; }
        public string? TeamAvatar { get; set; }

    }
}
