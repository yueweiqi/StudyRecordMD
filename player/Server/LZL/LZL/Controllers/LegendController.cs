using AutoMapper;
using LZL.DbModel.Model;
using LZL.DbModel.ModelDto.LegendEntityDto;
using LZL.DbModel.ModelDto.TeamEntityDto;
using LZL.DbModel.Utility;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace LZL.Controllers
{
    [Route("[controller]/[action]")]
    public class LegendController : Controller
    {
        #region 字段
        readonly IMongoDatabase _Database;
        readonly IMapper _Mapper;
        readonly string _LegendDbName = "legend";

        #endregion

        #region 构造函数
        public LegendController(IMongoDatabase database, IMapper mapper)
        {
            _Database = database;
            _Mapper = mapper;
        }
        #endregion

        #region 增删改
        [HttpPost]
        public IActionResult Add([FromBody] AddLegendEntityDto addLegendEntityDto)
        {
            var collection = _Database.GetCollection<LegendEntity>(_LegendDbName);

            var entity = _Mapper.Map<LegendEntity>(addLegendEntityDto);

            collection.InsertOne(entity);

            return Ok();
        }
        [HttpPost]
        public IActionResult Update([FromBody] UpdateLegendEntityDto updateLegendEntityDto)
        {
            var collection = _Database.GetCollection<LegendEntity>(_LegendDbName);
            var udpateDefinition = Builders<LegendEntity>.Update
                .Set(p => p.Name, updateLegendEntityDto.Name)
                .Set(p => p.Avatar, updateLegendEntityDto.Avatar);
                
            collection.UpdateOne(u => u.Id == updateLegendEntityDto.Id, udpateDefinition);

            return Ok();
        }
        [HttpPost]
        public IActionResult Delete([FromBody] DeleteLegendEntityDto deleteLegendEntityDto)
        {
            var collection = _Database.GetCollection<LegendEntity>(_LegendDbName);


            collection.DeleteOne(f => f.Id == deleteLegendEntityDto.Id);

            return Ok();
        }

        #endregion

        #region 查询
        [HttpGet]
        public IActionResult GetAll()
        {
            var collection = _Database.GetCollection<LegendEntity>(_LegendDbName);

            var teamEntityList = collection.Find(f => 1 == 1).ToList();

            var teamDtoList = _Mapper.Map<List<DataLegendEntityDto>>(teamEntityList);

            return Json(new { Data = teamDtoList });
        }

        [HttpPost]
        public IActionResult GetPageData([FromBody] SelectPageDataLegendEntityDto selectPageDataLegendEntityDto)
        {
            var collection = _Database.GetCollection<LegendEntity>(_LegendDbName);

            var whereCollection = collection.AsQueryable().Where(w => 1 == 1);
            if (!string.IsNullOrEmpty(selectPageDataLegendEntityDto.Name))
            {
                whereCollection = whereCollection.Where(w => w.Name.Contains(selectPageDataLegendEntityDto.Name));
            }

            var pageCount = whereCollection.Count();

            var dataList = whereCollection
                .Skip(selectPageDataLegendEntityDto.PageSize * (selectPageDataLegendEntityDto.PageIndex - 1))
                .Take(selectPageDataLegendEntityDto.PageSize).ToList();


            var mapList = _Mapper.Map<List<PageDataLegendEntityDto>>(dataList);
            PageDataOf<PageDataLegendEntityDto> pageData = new PageDataOf<PageDataLegendEntityDto>();
            pageData.PageIndex = selectPageDataLegendEntityDto.PageIndex;
            pageData.PageSize = selectPageDataLegendEntityDto.PageSize;
            pageData.TotalCount = pageCount;
            pageData.Items = mapList;
            return Json(new { Data = pageData });
        }
        #endregion

        #region 队伍头像上传
        [HttpPost]
        [HttpPost]
        public IActionResult AvatarPost([FromForm] IFormFile file)
        {
            string folderPath = "Resource/Legend/Avatar";
            string dirPath = AppContext.BaseDirectory + folderPath;
            if (!Directory.Exists(dirPath))
                Directory.CreateDirectory(dirPath);
            string suffix = file.FileName.Split(".")[file.FileName.Split(".").Length - 1];

            string fileReName = $"{Guid.NewGuid().ToString()}.{suffix}";

            string completeFileName = $"{dirPath}/{fileReName}";
            using (var stream = file.OpenReadStream())
            {
                using (FileStream fileStream = new FileStream(completeFileName, FileMode.Create, FileAccess.Write))
                {
                    stream.CopyTo(fileStream); // 将Stream内容复制到新文件
                }
            }
            return Json(new { Data = $"{folderPath}/{fileReName}" });
        }
        #endregion
    }
}
