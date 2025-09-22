using AutoMapper;
using LZL.DbModel.Enums;
using LZL.DbModel.Model;
using LZL.DbModel.ModelDto.CurrentMatchEntityDto;
using LZL.DbModel.ModelDto.MatchEntityDto;
using LZL.DbModel.ModelDto.PlayerEntityDto;
using LZL.DbModel.ModelDto.ProgressEntityDto;
using LZL.DbModel.Utility;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System.Xml;

namespace LZL.Controllers
{
    [Route("[controller]/[action]")]
    public class MatchController : Controller
    {
        #region 字段
        readonly IMongoDatabase _Database;
        readonly IMapper _Mapper;
        readonly string _TeamDbName = "team";
        readonly string _PlayerDbName = "player";
        readonly string _MatchName = "match";
        readonly string _ProgressName = "progress";
        readonly string _MatchPlayerCommentDbName = "match_player_comment";
        #endregion

        #region 构造函数
        public MatchController(IMongoDatabase database, IMapper mapper)
        {
            _Database = database;
            _Mapper = mapper;
        }
        #endregion

        [HttpGet]
        public IActionResult CurrentTeamInfo() 
        {
            #region 当前比赛队伍
            var cMCollection = _Database.GetCollection<DataMatchEntityDto>(_MatchName);

            var teamCollection=_Database.GetCollection<TeamEntity>(_TeamDbName);
            var playerCollection=_Database.GetCollection<DataPlayerEntityDto>(_PlayerDbName);
            DataProgressEntityDto dataCurrentMatch = new DataProgressEntityDto();

            #endregion

            var resultAggregate = cMCollection
               .Aggregate()
               .Match(Builders<DataMatchEntityDto>.Filter.Eq(e=>e.State,ProgressStateEnum.MatchProgress))
               .SortByDescending(o => o.StartTime)
               .Lookup(teamCollection, u => u.BlueId, t => t.Id, (DataMatchEntityDto m) => m.BlueTeam)
               .Lookup(teamCollection, u => u.RedId, t => t.Id, (DataMatchEntityDto m) => m.RedTeam)
               .ToList()
               .FirstOrDefault();
            dataCurrentMatch.CurrentMatch = resultAggregate;
            if (resultAggregate != null) 
            {
                #region 蓝队
                var blueList=playerCollection.Find(Builders<DataPlayerEntityDto>.Filter.And(
                        Builders<DataPlayerEntityDto>.Filter.Eq(e => e.TeamId, resultAggregate.BlueTeam[0].Id),
                        Builders<DataPlayerEntityDto>.Filter.Lte(e => e.Position, PositionEnum.SUP)
                    ))
                    .SortBy(s=>s.Position)
                    .ToList();

                var blueHeightList = blueList
                    .Select(s => new { s.Id, s.RankScore })
                    .OrderByDescending(s => s.RankScore)
                    .ToList();
                for (int i = 0; i < blueHeightList.Count; i++)
                {
                    blueList.FirstOrDefault(f => f.Id == blueHeightList[i].Id).Height = (i + 1);
                }

                dataCurrentMatch.BluePlayerList = blueList;
                #endregion

                #region 红队
                var redList = playerCollection.Find(Builders<DataPlayerEntityDto>.Filter.And(
                        Builders<DataPlayerEntityDto>.Filter.Eq(e => e.TeamId, resultAggregate.RedTeam[0].Id),
                        Builders<DataPlayerEntityDto>.Filter.Lte(e => e.Position, PositionEnum.SUP)
                    ))
                    .SortBy(s => s.Position)
                    .ToList();

                var redHeightList = redList
                    .Select(s => new { s.Id, s.RankScore })
                    .OrderByDescending(s => s.RankScore)
                    .ToList();
                for (int i = 0; i < redHeightList.Count; i++)
                {
                    redList.FirstOrDefault(f => f.Id == redHeightList[i].Id).Height = (i + 1);
                }


                dataCurrentMatch.RedPlayerList = redList;
                #endregion
            }
            return Json(new { Data= dataCurrentMatch});
        }

       

        #region 增删改
        [HttpPost]
        public IActionResult Add([FromBody] AddMatchEntityDto addMatchEntityDto)
        {
            var collection = _Database.GetCollection<MatchEntity>(_MatchName);
            
            var entity = _Mapper.Map<MatchEntity>(addMatchEntityDto);
            entity.State = ProgressStateEnum.None;
            collection.InsertOne(entity);

            return Ok();
        }
        [HttpPost]
        public IActionResult Update([FromBody] UpdateMatchEntityDto updateCurrentMatchPlayerDto)
        {
            var collection = _Database.GetCollection<MatchEntity>(_MatchName);

            var currentDefinition = Builders<MatchEntity>.Update
                .Set(p => p.Name, updateCurrentMatchPlayerDto.Name)
                .Set(p => p.BlueId, updateCurrentMatchPlayerDto.BlueId)
                .Set(p => p.BlueScore, updateCurrentMatchPlayerDto.BlueScore)
                .Set(p => p.RedId, updateCurrentMatchPlayerDto.RedId)
                .Set(p => p.RedScore, updateCurrentMatchPlayerDto.RedScore);

            collection.UpdateOne(w => w.Id == updateCurrentMatchPlayerDto.Id, currentDefinition);

            if (!string.IsNullOrEmpty(updateCurrentMatchPlayerDto.StartTimeStr)) 
            {
                DateTime dateTime = DateTime.Now;
                if (DateTime.TryParse(updateCurrentMatchPlayerDto.StartTimeStr, out dateTime)) 
                {
                    var currentDefinition2= Builders<MatchEntity>.Update
                            .Set(p => p.StartTime, dateTime);

                    collection.UpdateOne(w => w.Id == updateCurrentMatchPlayerDto.Id, currentDefinition2);
                }
               
            }
            if (!string.IsNullOrEmpty(updateCurrentMatchPlayerDto.EndTimeStr))
            {
                DateTime dateTime = DateTime.Now;
                if (DateTime.TryParse(updateCurrentMatchPlayerDto.EndTimeStr, out dateTime))
                {
                    var currentDefinition2 = Builders<MatchEntity>.Update
                            .Set(p => p.EndTime, dateTime);

                    collection.UpdateOne(w => w.Id == updateCurrentMatchPlayerDto.Id, currentDefinition2);
                }

            }
            return Ok();
        }

        [HttpPost]
        public IActionResult UpdateProgressState([FromBody] UpdateMatchProgressStateDto updateMatchProgressStateDto)
        {
            var collection = _Database.GetCollection<MatchEntity>(_MatchName);

            var mPlayerCollection = _Database.GetCollection<MatchPlayerCommentEntity>(_MatchPlayerCommentDbName);

            if (updateMatchProgressStateDto.State == ProgressStateEnum.MatchProgress)
            {
                #region 比赛修改
                {
                    var updateDefinition = Builders<MatchEntity>.Update
                  .Set(s => s.State, ProgressStateEnum.None);
                    collection.UpdateMany(f => 1 == 1, updateDefinition);

                    var updateDefinition2 = Builders<MatchEntity>.Update
                    .Set(s => s.State, ProgressStateEnum.MatchProgress);
                    collection.UpdateOne(f => f.Id == updateMatchProgressStateDto.Id, updateDefinition2);

                }

                #endregion

                #region 选手评论修改
                {
                    var updateDefinition = Builders<MatchPlayerCommentEntity>.Update
                   .Set(s => s.Match.State, ProgressStateEnum.None);
                    mPlayerCollection.UpdateMany(f => 1 == 1, updateDefinition);

                    var updateDefinition2 = Builders<MatchPlayerCommentEntity>.Update
                    .Set(s => s.Match.State, ProgressStateEnum.MatchProgress);
                    mPlayerCollection.UpdateMany(f => f.Match.Id == updateMatchProgressStateDto.Id, updateDefinition2);

                }
               
                #endregion
            }
            else if(updateMatchProgressStateDto.State == ProgressStateEnum.None)
            {
                #region 比赛修改
                {
                    var updateDefinition = Builders<MatchEntity>.Update
                    .Set(s => s.State, ProgressStateEnum.None);
                    collection.UpdateOne(f => f.Id == updateMatchProgressStateDto.Id, updateDefinition);
                }
                #endregion

                #region 选手评论修改
                {
                    var updateDefinition = Builders<MatchPlayerCommentEntity>.Update
                    .Set(s => s.Match.State, ProgressStateEnum.None);
                    mPlayerCollection.UpdateMany(f => f.Match.Id == updateMatchProgressStateDto.Id, updateDefinition);
                }
                #endregion
            }
            return Ok();
        }
        [HttpPost]
        public IActionResult Delete([FromBody] DeleteMatchEntityDto deleteMatchEntityDto)
        {
            var collection = _Database.GetCollection<MatchEntity>(_MatchName);


            collection.DeleteOne(f => f.Id ==deleteMatchEntityDto.Id);

            return Ok();
        }

        #endregion

        #region 查询
        [HttpPost]
        public IActionResult GetPageData([FromBody] SelectPageDataMatchEntityDto selectPageDataMatchEntityDto)
        {
            var collection = _Database.GetCollection<DataMatchEntityDto>(_MatchName);
            var teamCollection = _Database.GetCollection<TeamEntity>(_TeamDbName);
            var proCollection = _Database.GetCollection<ProgressEntity>(_ProgressName);

            #region 无用代码，作参考使用
            var lookupPipeline = new List<BsonDocument> {
                BsonDocument.Parse(@"{ $lookup: { 
                    from: 'customers', 
                    localField: 'customer_id', 
                    foreignField: '_id', 
                    as: 'customer' 
                }}")
            };
            #endregion

            #region 构建过滤器
            List<FilterDefinition<DataMatchEntityDto>> filterDefinitions = new();
            filterDefinitions.Add(FilterDefinition<DataMatchEntityDto>.Empty);
            if (!string.IsNullOrEmpty(selectPageDataMatchEntityDto.Name))
            {
                filterDefinitions.Add(Builders<DataMatchEntityDto>.Filter.Regex(m => m.Name, selectPageDataMatchEntityDto.Name));
            }
            if (!string.IsNullOrEmpty(selectPageDataMatchEntityDto.BlueId))
            {
                filterDefinitions.Add(Builders<DataMatchEntityDto>.Filter.Eq(m => m.BlueId, selectPageDataMatchEntityDto.BlueId));
            }
            if (!string.IsNullOrEmpty(selectPageDataMatchEntityDto.RedId))
            {
                filterDefinitions.Add(Builders<DataMatchEntityDto>.Filter.Eq(m => m.RedId, selectPageDataMatchEntityDto.RedId));
            }

            FilterDefinition<DataMatchEntityDto> filterDefinition= Builders<DataMatchEntityDto>.Filter.And(
                filterDefinitions
                );
            #endregion
            var resultAggregate = collection
                .Aggregate()
                .Match(filterDefinition)
                .SortByDescending(o=>o.StartTime)
                .Lookup(teamCollection, u => u.BlueId, t => t.Id, (DataMatchEntityDto m) => m.BlueTeam)
                .Lookup(teamCollection, u => u.RedId, t => t.Id, (DataMatchEntityDto m) => m.RedTeam);

            var pageCount = collection.CountDocuments(filterDefinition);

            var dataList= resultAggregate
                .Skip(selectPageDataMatchEntityDto.PageSize * (selectPageDataMatchEntityDto.PageIndex - 1))
                .Limit(selectPageDataMatchEntityDto.PageSize)
                .ToList(); // 执行聚合并获取结果列表

            var mapList = _Mapper.Map<List<PageDataMatchEntityDto>>(dataList);
            PageDataOf<PageDataMatchEntityDto> pageData = new PageDataOf<PageDataMatchEntityDto>();
            pageData.PageIndex = selectPageDataMatchEntityDto.PageIndex;
            pageData.PageSize = selectPageDataMatchEntityDto.PageSize;
            pageData.TotalCount = (int)pageCount;
            pageData.Items = mapList;
            return Json(new { Data = pageData });
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            #region 当前比赛队伍
            var cMCollection = _Database.GetCollection<DataMatchEntityDto>(_MatchName);

            var teamCollection = _Database.GetCollection<TeamEntity>(_TeamDbName);
            var playerCollection = _Database.GetCollection<DataPlayerEntityDto>(_PlayerDbName);

            List<DataProgressEntityDto> dataCurrentMatchList = new();

            #endregion

            var resultAggregate = cMCollection
               .Aggregate()
               //.Match(Builders<DataMatchEntityDto>.Filter.Eq(e => e.State, ProgressStateEnum.MatchProgress))
               .SortByDescending(o => o.StartTime)
               .Lookup(teamCollection, u => u.BlueId, t => t.Id, (DataMatchEntityDto m) => m.BlueTeam)
               .Lookup(teamCollection, u => u.RedId, t => t.Id, (DataMatchEntityDto m) => m.RedTeam)
               .ToList();

            foreach (var item in resultAggregate) 
            {
                DataProgressEntityDto dataCurrentMatch = new DataProgressEntityDto();
                dataCurrentMatch.CurrentMatch = item;
                if (item != null)
                {
                    #region 蓝队
                    var blueList = playerCollection.Find(Builders<DataPlayerEntityDto>.Filter.And(
                            Builders<DataPlayerEntityDto>.Filter.Eq(e => e.TeamId, item.BlueTeam[0].Id),
                            Builders<DataPlayerEntityDto>.Filter.Lte(e => e.Position, PositionEnum.SUP)
                        ))
                        .SortBy(s => s.Position)
                        .ToList();

                    var blueHeightList = blueList
                        .Select(s => new { s.Id, s.RankScore })
                        .OrderByDescending(s => s.RankScore)
                        .ToList();
                    for (int i = 0; i < blueHeightList.Count; i++)
                    {
                        blueList.FirstOrDefault(f => f.Id == blueHeightList[i].Id).Height = (i + 1);
                    }

                    dataCurrentMatch.BluePlayerList = blueList;
                    #endregion

                    #region 红队
                    var redList = playerCollection.Find(Builders<DataPlayerEntityDto>.Filter.And(
                            Builders<DataPlayerEntityDto>.Filter.Eq(e => e.TeamId, item.RedTeam[0].Id),
                            Builders<DataPlayerEntityDto>.Filter.Lte(e => e.Position, PositionEnum.SUP)
                        ))
                        .SortBy(s => s.Position)
                        .ToList();

                    var redHeightList = redList
                        .Select(s => new { s.Id, s.RankScore })
                        .OrderByDescending(s => s.RankScore)
                        .ToList();
                    for (int i = 0; i < redHeightList.Count; i++)
                    {
                        redList.FirstOrDefault(f => f.Id == redHeightList[i].Id).Height = (i + 1);
                    }


                    dataCurrentMatch.RedPlayerList = redList;
                    #endregion
                }
                dataCurrentMatchList.Add(dataCurrentMatch);
            }


           
            return Json(new { Data = dataCurrentMatchList });

        }
        #endregion
    }
}
