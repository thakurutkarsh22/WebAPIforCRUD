using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPIforCRUD.Models
{
    public class attendanceViewModel1
    {
        public string studend_id { get; set; }

        public string AttendanceMark { get; set; }

        public DateTime dateOfAttendance { get; set; }

        public string class_id { get; set; }
    }
}