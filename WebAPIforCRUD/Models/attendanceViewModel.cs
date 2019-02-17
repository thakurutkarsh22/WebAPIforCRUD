﻿using System;
using System.Collections.Generic;
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

        public string studend_id { get; set; }

        public Boolean AttendanceMark { get; set; }

        public DateTime dateOfAttendance { get; set; }
    }
}