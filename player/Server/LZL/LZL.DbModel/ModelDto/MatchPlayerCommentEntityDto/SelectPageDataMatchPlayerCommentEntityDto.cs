using LZL.DbModel.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LZL.DbModel.ModelDto.MatchPlayerCommentEntityDto
{
    public record SelectPageDataMatchPlayerCommentEntityDto:PageDataOf
    {
        public string? MatchId { get; set; }
    }
}
