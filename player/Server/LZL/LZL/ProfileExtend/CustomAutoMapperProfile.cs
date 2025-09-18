using AutoMapper;
using LZL.DbModel.Model;
using LZL.DbModel.ModelDto.CurrentMatchEntityDto;
using LZL.DbModel.ModelDto.MatchEntityDto;
using LZL.DbModel.ModelDto.PlayerEntityDto;
using LZL.DbModel.ModelDto.TeamEntityDto;
using MongoDB.Bson;

namespace LZL.ProfileExtend
{
    public class CustomAutoMapperProfile:Profile
    {
        public CustomAutoMapperProfile()
        {
            #region 选手
            CreateMap<AddPlayerEntityDto, PlayerEntity>()
                 .ForMember(dest => dest.SkilledHeros, map => map.MapFrom<List<string>>((src, dest) =>
                 {
                     if (string.IsNullOrEmpty(src.SkilledHeros))
                         return new List<string>();
                     else
                         return src.SkilledHeros.Split(',').ToList();
                 }))
                 .ForMember(dest => dest.TeamId, map => map.MapFrom(src => ObjectId.Parse(src.TeamId)));

            CreateMap<PlayerEntity, DataPlayerEntityDto>();

            CreateMap<PlayerEntity, PageDataPlayerEntityDto>()
                 .ForMember(dest => dest.Id, map => map.MapFrom(src => src.Id.ToString()))
                 .ForMember(dest => dest.TeamId, map => map.MapFrom(src => src.TeamId.ToString()));
            #endregion

            #region 队伍
            CreateMap<AddTeamEntityDto, TeamEntity>();

            CreateMap<TeamEntity, DataTeamEntityDto>()
                .ForMember(dest => dest.Id, map => map.MapFrom(src => src.Id.ToString()));

            CreateMap<TeamEntity, PageDataTeamEntityDto>()
                .ForMember(dest => dest.Id, map => map.MapFrom(src => src.Id.ToString()));
            #endregion

            #region 比赛
            CreateMap<AddMatchEntityDto, MatchEntity>()
                .ForMember(dest => dest.BlueId, map => map.MapFrom(src => ObjectId.Parse(src.BlueId)))
                .ForMember(dest => dest.RedId, map => map.MapFrom(src =>ObjectId.Parse(src.RedId)));
            CreateMap<MatchEntity, PageDataMatchEntityDto>()
                .ForMember(dest => dest.Id, map => map.MapFrom(src => src.Id.ToString()))
                .ForMember(dest => dest.BlueId, map => map.MapFrom(src => src.BlueId.ToString()))
                .ForMember(dest => dest.RedId, map => map.MapFrom(src => src.RedId.ToString()));

            CreateMap<DataMatchEntityDto, PageDataMatchEntityDto>();
            #endregion
        }
    }
}
