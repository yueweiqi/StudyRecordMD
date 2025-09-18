using LZL.DbModel.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LZL.DbModel.ModelDto.PlayerEntityDto
{
    public record UpdatePlayerEntityDto
    {
        public string Id { get; set; }
        /// <summary>
        /// 选手姓名
        /// </summary>
        public string Name { get; set; }

        public string TeamId { get; set; }
        /// <summary>
        /// 选手位置
        /// </summary>
        public PositionEnum Position { get; set; }
        /// <summary>
        /// 选手头像
        /// </summary>
        public string Avater { get; set; }
        /// <summary>
        /// rank分数
        /// </summary>
        public int RankScore { get; set; }
        /// <summary>
        /// rank段位
        /// </summary>
        public RankNameEnum RankName { get; set; }
        /// <summary>
        /// 学校名称
        /// </summary>
        public string School { get; set; }
        /// <summary>
        /// 擅长英雄
        /// </summary>
        public string? SkilledHeros { get; set; }
    }
}
