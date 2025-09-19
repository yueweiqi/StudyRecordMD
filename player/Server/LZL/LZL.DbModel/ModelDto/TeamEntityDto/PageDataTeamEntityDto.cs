using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LZL.DbModel.ModelDto.TeamEntityDto
{
    public record PageDataTeamEntityDto
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        /// <summary>
        /// 队伍名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 队伍头像
        /// </summary>
        public string Avatar { get; set; }

        /// <summary>
        /// 队伍宣言
        /// </summary>
        public string Manifesto { get; set; }

        /// <summary>
        /// 说明
        /// </summary>
        public string Description { get; set; }
    }
}
