using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace WebAPIforCRUD.Models
{
    public class classViewModel
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId _id { get; set; }
        
        public string ClassName { get; set; }
        
        public string StudentId { get; set; }
        
        public string UserId { get; set; }

    }
}