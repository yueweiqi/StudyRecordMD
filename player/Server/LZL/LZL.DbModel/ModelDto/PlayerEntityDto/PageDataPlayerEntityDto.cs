
using LZL.DbModel.Enums;
using LZL.DbModel.Extension;
using LZL.DbModel.ModelDto.TeamEntityDto;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LZL.DbModel.Model;

namespace LZL.DbModel.ModelDto.PlayerEntityDto
{
    public record PageDataPlayerEntityDto
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public string TeamId { get; set; }
        public List<DataTeamEntityDto> Team { get; set; }
        /// <summary>
        /// 选手姓名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 选手位置
        /// </summary>
        public PositionEnum Position { get; set; }

        public string? PositionStr { get {
                return Position.GetDescription();
            } }
        /// <summary>
        /// 选手头像
        /// </summary>
        public string Avater { get; set; }
        [BsonDefaultValue(PlayerIdentityEnum.None)]
        public PlayerIdentityEnum Identity { get; set; }
        public string? IdentityStr
        {
            get
            {
                return Identity.GetDescription();
            }
        }
        /// <summary>
        /// rank分数
        /// </summary>
        public int RankScore { get; set; }
        /// <summary>
        /// rank段位
        /// </summary>
        public RankNameEnum RankName { get; set; }
        public string? RankNameStr { get {
                return RankName.GetDescription();
            } }
        /// <summary>
        /// 学校名称
        /// </summary>
        public string School { get; set; }
        /// <summary>
        /// 擅长英雄
        /// </summary>
        public List<LegendEntity> SkilledHeros { get; set; } = new List<LegendEntity>();
    }
}
