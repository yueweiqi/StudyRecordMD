using LZL.DbModel.Enums;
using LZL.DbModel.Utility;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LZL.DbModel.ModelDto.VideoEntityDto
{
    public record PageDataVideoEntityDto
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public DateTime? StartTime { get; set; }

        /// <summary>
        /// 数据库存储时间为UTC时间,这里转换为携带当前时区增量的标准时间
        /// </summary>
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
        public string? VideoUrl { get; set; }
        public ProgressStateEnum State { get; set; }
    }
}
