﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPIforCRUD.Models
{
    public class attendanceViewModel1
    {
       
        public string studend_id { get; set; }

        public string stu_name { get; set; }

        public Boolean AttendanceMark { get; set; }

        public string class_id { get; set; }

        public string date { get; set; }

        public attendanceViewModel1(string student_id1 , string stu_name1 , Boolean attendanceMark, string class_id1 , string date)
        {
            this.studend_id = student_id1;
            this.stu_name = stu_name1;
            this.AttendanceMark = attendanceMark;
            this.class_id = class_id1;
            this.date = date;
        }
    }
}