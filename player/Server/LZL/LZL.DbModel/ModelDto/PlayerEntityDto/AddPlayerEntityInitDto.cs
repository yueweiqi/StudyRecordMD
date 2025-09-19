using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LZL.DbModel.ModelDto.PlayerEntityDto
{
    public record DataPositionEnumDto 
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
    public record DataRankNameEnumDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
    public record DataPlayerIdentityEnumDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
    public record AddPlayerEntityInitDto
    {
        public List<DataPositionEnumDto> DataPositionEnumDtos { get; set; } = new();
        public List<DataRankNameEnumDto> DataRankNameEnumDtos { get; set; } = new();

        public List<DataPlayerIdentityEnumDto> DataPlayerIdentityEnumDtos { get; set; } = new();
    }
}
