using LZL.DbModel.Enums;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LZL.DbModel.ModelDto.MatchEntityDto
{
    public record AddMatchEntityDto
    {
        /// <summary>
        /// 比赛名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 蓝方
        /// </summary>
        public string BlueId { get; set; }
        public int BlueScore { get; set; }
        /// <summary>
        /// 红方
        /// </summary>
        public string RedId { get; set; }
        public int RedScore { get; set; }

        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public ProgressStateEnum State { get; set; }
    }
}
