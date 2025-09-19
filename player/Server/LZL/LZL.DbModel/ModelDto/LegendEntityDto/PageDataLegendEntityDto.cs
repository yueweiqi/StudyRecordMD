using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LZL.DbModel.Utility;

namespace LZL.DbModel.ModelDto.LegendEntityDto
{
    public record PageDataLegendEntityDto:PageDataOf
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }

        public string Name { get; set; }

        public string Avatar { get; set; }

       
    }
}
