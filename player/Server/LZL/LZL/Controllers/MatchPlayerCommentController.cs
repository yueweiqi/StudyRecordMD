using AutoMapper;
using LZL.DbModel.Enums;
using LZL.DbModel.Model;
using LZL.DbModel.ModelDto.CurrentMatchEntityDto;
using LZL.DbModel.ModelDto.MatchEntityDto;
using LZL.DbModel.ModelDto.MatchPlayerCommentEntityDto;
using LZL.DbModel.ModelDto.PlayerEntityDto;
using LZL.DbModel.ModelDto.ProgressEntityDto;
using LZL.DbModel.ModelDto.TeamEntityDto;
using LZL.DbModel.ModelDto.VideoEntityDto;
using LZL.DbModel.Utility;
using LZL.ProfileExtend;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace LZL.Controllers
{
    [Route("[controller]/[action]")]
    public class MatchPlayerCommentController : Controller
    {
        #region 字段
        readonly IMongoDatabase _Database;
        readonly IMapper _Mapper;
        readonly string _MatchPlayerCommentDbName = "match_player_comment";

        #endregion

        #region 构造函数
        public MatchPlayerCommentController(IMongoDatabase database, IMapper mapper)
        {
            _Database = database;
            _Mapper = mapper;
        }
        #endregion

        #region 增删改
        [HttpPost]
        public IActionResult Add([FromBody] AddMatchPlayerCommentEntityDto addMatchPlayerCommentEntityDto)
        {
            var collection = _Database.GetCollection<MatchPlayerCommentEntity>(_MatchPlayerCommentDbName);

            var entity = _Mapper.Map<MatchPlayerCommentEntity>(addMatchPlayerCommentEntityDto);

            collection.InsertOne(entity);

            return Ok();
        }
        [HttpPost]
        public IActionResult Update([FromBody] UpdateMatchPlayerCommentEntityDto updateMatchPlayerCommentEntityDto)
        {
            var collection = _Database.GetCollection<MatchPlayerCommentEntity>(_MatchPlayerCommentDbName);
            var udpateDefinition = Builders<MatchPlayerCommentEntity>.Update
                .Set(p => p.Name, updateMatchPlayerCommentEntityDto.Name)
                .Set(p => p.Match, updateMatchPlayerCommentEntityDto.Match)
                .Set(p => p.BlueTeam, updateMatchPlayerCommentEntityDto.BlueTeam)
                .Set(p => p.BluePlayerList, updateMatchPlayerCommentEntityDto.BluePlayerList)
                .Set(p => p.RedTeam, updateMatchPlayerCommentEntityDto.RedTeam)
                .Set(p => p.RedPlayerList, updateMatchPlayerCommentEntityDto.RedPlayerList);

            collection.UpdateOne(u => u.Id == updateMatchPlayerCommentEntityDto.Id, udpateDefinition);
            return Ok();
        }
        [HttpPost]
        public IActionResult Delete([FromBody] DeleteMatchPlayerCommentEntityDto deleteMatchPlayerCommentEntityDto)
        {
            var collection = _Database.GetCollection<MatchPlayerCommentEntity>(_MatchPlayerCommentDbName);


            collection.DeleteOne(f => f.Id == deleteMatchPlayerCommentEntityDto.Id);

            return Ok();
        }

        #endregion

        #region 查询


        [HttpPost]
        public IActionResult GetPageData([FromBody] SelectPageDataMatchPlayerCommentEntityDto selectPageDataMatchPlayerCommentEntityDto)
        {
            var collection = _Database.GetCollection<MatchPlayerCommentEntity>(_MatchPlayerCommentDbName);

            var whereCollection = collection.AsQueryable().Where(w => 1 == 1);

            if (!string.IsNullOrEmpty(selectPageDataMatchPlayerCommentEntityDto.MatchId)) 
            {
                whereCollection=whereCollection.Where(w=>w.Match.Id==selectPageDataMatchPlayerCommentEntityDto.MatchId);
            }

            var pageCount = whereCollection.Count();

            var dataList = whereCollection
                .Skip(selectPageDataMatchPlayerCommentEntityDto.PageSize * (selectPageDataMatchPlayerCommentEntityDto.PageIndex - 1))
                .Take(selectPageDataMatchPlayerCommentEntityDto.PageSize).ToList();


            var mapList = _Mapper.Map<List<PageDataMatchPlayerCommentEntityDto>>(dataList);
            PageDataOf<PageDataMatchPlayerCommentEntityDto> pageData = new PageDataOf<PageDataMatchPlayerCommentEntityDto>();
            pageData.PageIndex = selectPageDataMatchPlayerCommentEntityDto.PageIndex;
            pageData.PageSize = selectPageDataMatchPlayerCommentEntityDto.PageSize;
            pageData.TotalCount = pageCount;
            pageData.Items = mapList;
            return Json(new { Data = pageData });
        }
        #endregion


        [HttpPost]
        public IActionResult UpdateProgressState([FromBody] UpdateMatchPlayerCommentStateDto updateMatchPlayerCommentStateDto)
        {
            var mPlayerCollection = _Database.GetCollection<MatchPlayerCommentEntity>(_MatchPlayerCommentDbName);

            if (updateMatchPlayerCommentStateDto.State == ProgressStateEnum.MatchProgress)
            {

                #region 选手评论修改
                {
                    var updateDefinition = Builders<MatchPlayerCommentEntity>.Update
                    .Set(s => s.State, ProgressStateEnum.None);

                    mPlayerCollection.UpdateMany(f => f.Match.Id==updateMatchPlayerCommentStateDto.MatchId, updateDefinition);

                    var updateDefinition2 = Builders<MatchPlayerCommentEntity>.Update
                    .Set(s => s.State, ProgressStateEnum.MatchProgress);
                    mPlayerCollection.UpdateOne(f => f.Id == updateMatchPlayerCommentStateDto.Id, updateDefinition2);

                }

                #endregion
            }
            else if (updateMatchPlayerCommentStateDto.State == ProgressStateEnum.None)
            {

                #region 选手评论修改
                {
                    var updateDefinition = Builders<MatchPlayerCommentEntity>.Update
                    .Set(s => s.State, ProgressStateEnum.None);
                    mPlayerCollection.UpdateOne(f => f.Id == updateMatchPlayerCommentStateDto.Id, updateDefinition);
                }
                #endregion
            }
            return Ok();
        }

        [HttpGet]
        public IActionResult CurrentMatchPlayerCommentInfo()
        {
            #region 当前比赛队伍
            var cMatchCollection = _Database.GetCollection<MatchEntity>(ConstDbName.MatchName);
            
            var cMatchEntity=cMatchCollection.Find(f=>f.State==ProgressStateEnum.MatchProgress)
                .ToList()
                .FirstOrDefault();
            #endregion

            if (cMatchEntity != null) 
            {
                var cMPlayerCollection=_Database.GetCollection<DataMatchPlayerCommentEntityDto>(ConstDbName.MatchPlayerCommentDbName);

                var cMPlayerEntity=cMPlayerCollection
                    .Find(f=>f.State==ProgressStateEnum.MatchProgress&&f.Match.State == ProgressStateEnum.MatchProgress)
                    .ToList()
                    .FirstOrDefault();

                DataCurrentMatchPlayerEntityDto data = new DataCurrentMatchPlayerEntityDto();
                List<TeamPlayerCommentInfoDto> list = new();
                foreach (var player in cMPlayerEntity.BluePlayerList) 
                {
                    TeamPlayerCommentInfoDto teamPlayer = _Mapper.Map<TeamPlayerCommentInfoDto>(player);
                    teamPlayer.TeamName = cMPlayerEntity.BlueTeam.Name;
                    teamPlayer.TeamAvatar = cMPlayerEntity.BlueTeam.Avatar;
                    list.Add(teamPlayer);
                }
                foreach (var player in cMPlayerEntity.RedPlayerList)
                {
                    TeamPlayerCommentInfoDto teamPlayer = _Mapper.Map<TeamPlayerCommentInfoDto>(player);
                    teamPlayer.TeamName = cMPlayerEntity.RedTeam.Name;
                    teamPlayer.TeamAvatar = cMPlayerEntity.RedTeam.Avatar;
                    list.Add(teamPlayer);
                }
                data.PlayerList = list;
                data.Id = cMPlayerEntity.Id;
                data.Name=cMPlayerEntity.Name;
                return Json(new { Data= data });
            }
            return Json(new {});
        }

    }
}
