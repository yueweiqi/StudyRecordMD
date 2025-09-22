using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LZL.DbModel.ModelDto.VideoEntityDto
{
    public record UpdateVideoEntityDto
    {
        public string Id { get; set; }
        public string? StartTimeStr { get; set; }

        public string? VideoUrl { get; set; }
    }
}
