using LZL.DbModel.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LZL.DbModel.ModelDto.PlayerEntityDto
{
    public record SelectPageDataPlayerEntityDto: PageDataOf
    {
        public string? TeamId { get; set; }

        public string? Name { get; set; }
    }
}
