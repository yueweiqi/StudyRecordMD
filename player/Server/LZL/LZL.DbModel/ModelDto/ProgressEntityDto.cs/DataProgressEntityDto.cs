using LZL.DbModel.ModelDto.CurrentMatchEntityDto;
using LZL.DbModel.ModelDto.PlayerEntityDto;
using LZL.DbModel.ModelDto.TeamEntityDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LZL.DbModel.ModelDto.ProgressEntityDto.cs
{
    public record DataProgressEntityDto
    {
        public DataMatchEntityDto CurrentMatch { get; set; }

        public List<DataPlayerEntityDto> BluePlayerList { get; set; } = new();

        public List<DataPlayerEntityDto> RedPlayerList { get; set; } = new();
    }
}
