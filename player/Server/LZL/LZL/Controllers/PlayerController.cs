using AutoMapper;
using LZL.DbModel.Enums;
using LZL.DbModel.Extension;
using LZL.DbModel.Model;
using LZL.DbModel.ModelDto.CurrentMatchEntityDto;
using LZL.DbModel.ModelDto.PlayerEntityDto;
using LZL.DbModel.ModelDto.TeamEntityDto;
using LZL.DbModel.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Extensions;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace LZL.Controllers
{

    [Route("[controller]/[action]")]
    public class PlayerController : Controller
    {
        #region 字段
        readonly IMongoDatabase _Database;
        readonly IMapper _Mapper;
        readonly string _PlayerDbName = "player";
        readonly string _TeamDbName = "team";
        #endregion

        #region 构造函数
        public PlayerController(IMongoDatabase database, IMapper mapper)
        {
            _Database = database;
            _Mapper = mapper;
        }
        #endregion
        [HttpPost]
        public IActionResult Add([FromBody] AddPlayerEntityDto addPlayerEntityDto) 
        {
            var collection= _Database.GetCollection<PlayerEntity>(_PlayerDbName);

            var entity= _Mapper.Map<PlayerEntity>(addPlayerEntityDto);

            collection.InsertOne(entity);

            var ddfd = collection.Find(f => 1 == 1).FirstOrDefault();

            return Ok();
        }
        [HttpPost]
        public IActionResult Update([FromBody] UpdatePlayerEntityDto updatePlayerEntityDto)
        {
            var collection = _Database.GetCollection<PlayerEntity>(_PlayerDbName);


            var herosList = new List<string>();
            if (!string.IsNullOrEmpty(updatePlayerEntityDto.SkilledHeros))
                herosList = updatePlayerEntityDto.SkilledHeros.Split(",").ToList();

            var udpateDefinition = Builders<PlayerEntity>.Update
                .Set(p => p.Name, updatePlayerEntityDto.Name)
                .Set(p => p.TeamId, updatePlayerEntityDto.TeamId)
                .Set(p => p.Position, updatePlayerEntityDto.Position)
                .Set(p => p.RankName, updatePlayerEntityDto.RankName)
                .Set(p => p.RankScore, updatePlayerEntityDto.RankScore)
                .Set(p => p.School, updatePlayerEntityDto.School)
                .Set(p => p.SkilledHeros, herosList);
            collection.UpdateOne(u=>u.Id==updatePlayerEntityDto.Id, udpateDefinition);

            return Ok();
        }
        [HttpPost]
        public IActionResult Delete([FromBody] DeletePlayerEntityDto deletePlayerEntityDto)
        {
            var collection = _Database.GetCollection<PlayerEntity>(_PlayerDbName);


            collection.DeleteOne(f => f.Id == deletePlayerEntityDto.Id);

            return Ok();
        }
        [HttpGet]
        public IActionResult GetAddInit() 
        {
            AddPlayerEntityInitDto addPlayerEntityInitDto = new AddPlayerEntityInitDto();

            List<DataPositionEnumDto> pList = new();
            foreach (PositionEnum positionEnum in Enum.GetValues(typeof(PositionEnum))) 
            {
                pList.Add(new DataPositionEnumDto()
                {
                    Id = (int)positionEnum,
                    Name = positionEnum.ToString(),
                    Description = positionEnum.GetDescription()
                });
            }

            List<DataRankNameEnumDto> rList = new();
            foreach (RankNameEnum positionEnum in Enum.GetValues(typeof(RankNameEnum)))
            {
                rList.Add(new DataRankNameEnumDto()
                {
                    Id = (int)positionEnum,
                    Name = positionEnum.ToString(),
                    Description = positionEnum.GetDescription()
                });
            }
            addPlayerEntityInitDto.DataPositionEnumDtos = pList;
            addPlayerEntityInitDto.DataRankNameEnumDtos= rList;
            return Json(new { Data = addPlayerEntityInitDto });
        }
        [HttpPost]
        public IActionResult GetPageData([FromBody] SelectPageDataPlayerEntityDto selectPageDataPlayerEntityDto)
        {
            var playerCollection = _Database.GetCollection<PageDataPlayerEntityDto>(_PlayerDbName);
            var teamCollection = _Database.GetCollection<TeamEntity>(_TeamDbName);

            #region 构建过滤器
            List<FilterDefinition<PageDataPlayerEntityDto>> filterDefinitions = new();
            filterDefinitions.Add(FilterDefinition<PageDataPlayerEntityDto>.Empty);

            if (!string.IsNullOrEmpty(selectPageDataPlayerEntityDto.Name))
            {
                filterDefinitions.Add(Builders<PageDataPlayerEntityDto>.Filter.Regex(w => w.Name,selectPageDataPlayerEntityDto.Name));
            }
            if (!string.IsNullOrEmpty(selectPageDataPlayerEntityDto.TeamId))
            {
                filterDefinitions.Add(Builders<PageDataPlayerEntityDto>.Filter.Eq(w => w.TeamId, selectPageDataPlayerEntityDto.TeamId));
            }
            FilterDefinition<PageDataPlayerEntityDto> filterDefinition = Builders<PageDataPlayerEntityDto>.Filter.And(
                filterDefinitions
                );
            #endregion

            var pageCount = playerCollection.CountDocuments(filterDefinition);

            var dataList=playerCollection
                .Aggregate()
                .Match(filterDefinition)
                .SortByDescending(s=>s.TeamId)
                .Lookup(teamCollection, f => f.TeamId, t => t.Id, (PageDataPlayerEntityDto player) => player.Team)
                .Skip(selectPageDataPlayerEntityDto.PageSize * (selectPageDataPlayerEntityDto.PageIndex - 1))
                .Limit(selectPageDataPlayerEntityDto.PageSize)
                .ToList();            
            var mapList = _Mapper.Map<List<PageDataPlayerEntityDto>>(dataList);
            PageDataOf<PageDataPlayerEntityDto> pageData = new PageDataOf<PageDataPlayerEntityDto>();
            pageData.PageIndex = selectPageDataPlayerEntityDto.PageIndex;
            pageData.PageSize=selectPageDataPlayerEntityDto.PageSize;
            pageData.TotalCount = (int)pageCount;
            pageData.Items = mapList;
            return Json(new { Data= pageData });
        }


    }
}
