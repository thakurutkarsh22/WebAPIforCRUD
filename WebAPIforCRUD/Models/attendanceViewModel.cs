using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace WebAPIforCRUD.Models
{
    public class attendanceViewModel
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId _id { get; set; }

        [Required]
        public string studend_id { get; set; }
        [Required]
        public string AttendanceMark { get; set; }
        [Required]
        public DateTime dateOfAttendance { get; set; }
        [Required]
        public string class_id { get; set; }
    }
}