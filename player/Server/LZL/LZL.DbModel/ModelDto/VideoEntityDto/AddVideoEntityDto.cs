using LZL.DbModel.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LZL.DbModel.ModelDto.VideoEntityDto
{
    public record AddVideoEntityDto
    {
        public string? StartTime { get; set; }

        public string? VideoUrl { get; set; }

        public ProgressStateEnum State { get; set; }
    }
}
