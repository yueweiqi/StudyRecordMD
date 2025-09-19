using LZL.DbModel.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LZL.DbModel.ModelDto.TeamEntityDto
{
    public record SelectPageDataTeamEntityDto:PageDataOf
    {
        public string? Name { get; set; }
    }
}
