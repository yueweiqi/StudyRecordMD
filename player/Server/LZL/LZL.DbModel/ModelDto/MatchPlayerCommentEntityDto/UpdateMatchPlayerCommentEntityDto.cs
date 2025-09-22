using LZL.DbModel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LZL.DbModel.ModelDto.MatchPlayerCommentEntityDto
{
    public record UpdateMatchPlayerCommentEntityDto
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
        public MatchCommentInfoDto Match { get; set; }

        public TeamCommentInfoDto BlueTeam { get; set; }

        public List<PlayerCommentInfoDto> BluePlayerList { get; set; } = new();

        public TeamCommentInfoDto RedTeam { get; set; }
        public List<PlayerCommentInfoDto> RedPlayerList { get; set; } = new();
    }
}
