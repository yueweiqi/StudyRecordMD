using AutoMapper;
using LZL.DbModel.Enums;
using LZL.DbModel.Model;
using LZL.DbModel.ModelDto.CurrentMatchEntityDto;
using LZL.DbModel.ModelDto.MatchEntityDto;
using LZL.DbModel.ModelDto.PlayerEntityDto;
using LZL.DbModel.ModelDto.ProgressEntityDto;
using LZL.DbModel.ModelDto.TeamEntityDto;
using LZL.DbModel.ModelDto.VideoEntityDto;
using LZL.DbModel.Utility;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace LZL.Controllers
{
    [Route("[controller]/[action]")]
    public class VideoController : Controller
    {
        #region 字段
        readonly IMongoDatabase _Database;
        readonly IMapper _Mapper;
        readonly string _VideoDbName = "video";

        #endregion

        #region 构造函数
        public VideoController(IMongoDatabase database, IMapper mapper)
        {
            _Database = database;
            _Mapper = mapper;
        }
        #endregion

        #region 增删改
        [HttpPost]
        public IActionResult Add([FromBody] AddVideoEntityDto addVideoEntityDto)
        {
            var collection = _Database.GetCollection<VideoEntity>(_VideoDbName);

            var entity = _Mapper.Map<VideoEntity>(addVideoEntityDto);

            collection.InsertOne(entity);

            return Ok();
        }
        [HttpPost]
        public IActionResult Update([FromBody] UpdateVideoEntityDto updateVideoEntityDto)
        {
            var collection = _Database.GetCollection<VideoEntity>(_VideoDbName);
            var udpateDefinition = Builders<VideoEntity>.Update
                .Set(p => p.VideoUrl, updateVideoEntityDto.VideoUrl);
            collection.UpdateOne(u => u.Id == updateVideoEntityDto.Id, udpateDefinition);

            if (!string.IsNullOrEmpty(updateVideoEntityDto.StartTimeStr))
            {
                DateTime dateTime = DateTime.Now;
                if (DateTime.TryParse(updateVideoEntityDto.StartTimeStr, out dateTime))
                {
                    var currentDefinition2 = Builders<VideoEntity>.Update
                            .Set(p => p.StartTime, dateTime);

                    collection.UpdateOne(w => w.Id == updateVideoEntityDto.Id, currentDefinition2);
                }

            }
            return Ok();
        }
        [HttpPost]
        public IActionResult Delete([FromBody] DeleteVideoEntityDto deleteVideoEntityDto)
        {
            var collection = _Database.GetCollection<VideoEntity>(_VideoDbName);


            collection.DeleteOne(f => f.Id == deleteVideoEntityDto.Id);

            return Ok();
        }
        [HttpPost]
        public IActionResult UpdateProgressState([FromBody] UpdateVideoProgressStateDto updateVideoProgressStateDto)
        {
            var collection = _Database.GetCollection<VideoEntity>(_VideoDbName);

            if (updateVideoProgressStateDto.State == ProgressStateEnum.MatchProgress)
            {
                var updateDefinition = Builders<VideoEntity>.Update
                .Set(s => s.State, ProgressStateEnum.None);
                collection.UpdateMany(f => 1 == 1, updateDefinition);

                var updateDefinition2 = Builders<VideoEntity>.Update
                .Set(s => s.State, ProgressStateEnum.MatchProgress);
                collection.UpdateOne(f => f.Id == updateVideoProgressStateDto.Id, updateDefinition2);
            }
            else if (updateVideoProgressStateDto.State == ProgressStateEnum.None)
            {
                var updateDefinition = Builders<VideoEntity>.Update
               .Set(s => s.State, ProgressStateEnum.None);
                collection.UpdateOne(f => f.Id == updateVideoProgressStateDto.Id, updateDefinition);
            }
            return Ok();
        }

        #endregion

        #region 查询


        [HttpPost]
        public IActionResult GetPageData([FromBody] SelectPageDataVideoEntityDto selectPageDataVideoEntityDto)
        {
            var collection = _Database.GetCollection<VideoEntity>(_VideoDbName);

            var whereCollection = collection.AsQueryable().Where(w => 1 == 1);


            var pageCount = whereCollection.Count();

            var dataList = whereCollection
                .Skip(selectPageDataVideoEntityDto.PageSize * (selectPageDataVideoEntityDto.PageIndex - 1))
                .Take(selectPageDataVideoEntityDto.PageSize).ToList();


            var mapList = _Mapper.Map<List<PageDataVideoEntityDto>>(dataList);
            PageDataOf<PageDataVideoEntityDto> pageData = new PageDataOf<PageDataVideoEntityDto>();
            pageData.PageIndex = selectPageDataVideoEntityDto.PageIndex;
            pageData.PageSize = selectPageDataVideoEntityDto.PageSize;
            pageData.TotalCount = pageCount;
            pageData.Items = mapList;
            return Json(new { Data = pageData });
        }
        #endregion


       private string folderPath = "Resource/Video/Video/";

        #region 队伍头像上传
        [HttpPost]
        [HttpPost]
        public IActionResult AvatarPost([FromForm] IFormFile file)
        {
            
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

        [HttpGet]
        public IActionResult CurrentTeamInfo()
        {
            #region 当前比赛队伍
            var cMCollection = _Database.GetCollection<DataVideoEnityDto>(_VideoDbName);

            var entity= cMCollection.Find(f => f.State == ProgressStateEnum.MatchProgress).ToList().FirstOrDefault();

            if (entity!=null)
            {
                entity.VideoUrl = folderPath + entity.VideoUrl;
            }
            

            #endregion

            return Json(new { Data = entity });
        }

    }
}
