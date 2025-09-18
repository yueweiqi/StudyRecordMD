using AutoMapper;
using LZL.DbModel.Model;
using LZL.DbModel.ModelDto.PlayerEntityDto;
using LZL.DbModel.ModelDto.TeamEntityDto;
using LZL.DbModel.Utility;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using System.IO;

namespace LZL.Controllers
{
    [Route("[controller]/[action]")]
    public class TeamController : Controller
    {

        #region 字段
        readonly IMongoDatabase _Database;
        readonly IMapper _Mapper;
        readonly string _TeamDbName = "team";

        #endregion

        #region 构造函数
        public TeamController(IMongoDatabase database, IMapper mapper)
        {
            _Database = database;
            _Mapper = mapper;
        }
        #endregion

        #region 增删改
        [HttpPost]
        public IActionResult Add([FromBody] AddTeamEntityDto addTeamEntityDto)
        {
            var collection = _Database.GetCollection<TeamEntity>(_TeamDbName);

            var entity = _Mapper.Map<TeamEntity>(addTeamEntityDto);

            collection.InsertOne(entity);

            return Ok();
        }
        [HttpPost]
        public IActionResult Update([FromBody] UpdateTeamEntityDto updateTeamEntityDto)
        {
            var collection = _Database.GetCollection<TeamEntity>(_TeamDbName);
            var udpateDefinition = Builders<TeamEntity>.Update
                .Set(p => p.Name, updateTeamEntityDto.Name)
                .Set(p => p.Avater, updateTeamEntityDto.Avater)
                .Set(p => p.Manifesto, updateTeamEntityDto.Manifesto)
                .Set(p => p.Description, updateTeamEntityDto.Description);
            collection.UpdateOne(u => u.Id ==updateTeamEntityDto.Id, udpateDefinition);

            return Ok();
        }
        [HttpPost]
        public IActionResult Delete([FromBody] DeleteTeamEntityDto deleteTeamEntityDto)
        {
            var collection = _Database.GetCollection<TeamEntity>(_TeamDbName);


            collection.DeleteOne(f => f.Id == deleteTeamEntityDto.Id);

            return Ok();
        }

        #endregion

        #region 查询
        [HttpGet]
        public IActionResult GetAll()
        {
            var collection = _Database.GetCollection<TeamEntity>(_TeamDbName);

            var teamEntityList = collection.Find(f => 1 == 1).ToList();

            var teamDtoList = _Mapper.Map<List<DataTeamEntityDto>>(teamEntityList);

            return Json(new { Data = teamDtoList });
        }

        [HttpPost]
        public IActionResult GetPageData([FromBody] SelectPageDataTeamEntityDto selectPageDataTeamEntityDto)
        {
            var collection = _Database.GetCollection<TeamEntity>(_TeamDbName);

            var whereCollection = collection.AsQueryable().Where(w => 1 == 1);
            if (!string.IsNullOrEmpty(selectPageDataTeamEntityDto.Name))
            {
                whereCollection = whereCollection.Where(w => w.Name.Contains(selectPageDataTeamEntityDto.Name));
            }
            
            var pageCount = whereCollection.Count();

            var dataList = whereCollection
                .Skip(selectPageDataTeamEntityDto.PageSize * (selectPageDataTeamEntityDto.PageIndex - 1))
                .Take(selectPageDataTeamEntityDto.PageSize).ToList();


            var mapList = _Mapper.Map<List<PageDataTeamEntityDto>>(dataList);
            PageDataOf<PageDataTeamEntityDto> pageData = new PageDataOf<PageDataTeamEntityDto>();
            pageData.PageIndex = selectPageDataTeamEntityDto.PageIndex;
            pageData.PageSize = selectPageDataTeamEntityDto.PageSize;
            pageData.TotalCount = pageCount;
            pageData.Items = mapList;
            return Json(new { Data = pageData });
        }
        #endregion

        #region 队伍头像上传
        [HttpPost]
        [HttpPost]
        public IActionResult AvaterPost([FromForm] IFormFile file)
        {
            string folderPath = "Resource/Team/Avater";
            string dirPath = AppContext.BaseDirectory + folderPath;            
            if(!Directory.Exists(dirPath))
                Directory.CreateDirectory(dirPath);
            string suffix = file.FileName.Split(".")[file.FileName.Split(".").Length - 1];

            string fileReName = $"{Guid.NewGuid().ToString()}.{suffix}";

            string completeFileName = $"{dirPath}/{fileReName}";
            using (var stream=file.OpenReadStream()) 
            {
                using (FileStream fileStream = new FileStream(completeFileName, FileMode.Create, FileAccess.Write))
                {
                    stream.CopyTo(fileStream); // 将Stream内容复制到新文件
                }
            }
            return Json(new {Data= $"{folderPath}/{fileReName}" });
        }
        #endregion
    }
}
