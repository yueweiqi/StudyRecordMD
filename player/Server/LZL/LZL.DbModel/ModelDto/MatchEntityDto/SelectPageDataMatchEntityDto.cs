using LZL.DbModel.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LZL.DbModel.ModelDto.MatchEntityDto
{
    public record SelectPageDataMatchEntityDto:PageDataOf
    {
        public string? Name { get; set; }

        public string? BlueId { get; set; }
        public string? RedId { get; set; }
    }
}
