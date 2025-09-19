using LZL.DbModel.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LZL.DbModel.ModelDto.MatchEntityDto
{
    public record UpdateMatchProgressStateDto
    {
        public string? Id { get; set; }
        public ProgressStateEnum State { get; set; }
    }
}
