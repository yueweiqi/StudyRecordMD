using LZL.DbModel.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LZL.DbModel.ModelDto.MatchPlayerCommentEntityDto
{
    public record UpdateMatchPlayerCommentStateDto
    {
        public string? Id { get; set; }
        public string? MatchId { get; set; }
        public ProgressStateEnum State { get; set; }
    }
}
