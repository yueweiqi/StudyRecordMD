using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LZL.DbModel.ModelDto.LegendEntityDto
{
    public record AddLegendEntityDto
    {
        public string Name { get; set; }

        public string Avatar { get; set; }
    }
}
