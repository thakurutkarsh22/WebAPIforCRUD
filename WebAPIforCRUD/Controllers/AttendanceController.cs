using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MongoDB.Driver;
using WebAPIforCRUD.App_Start;
using WebAPIforCRUD.Models;
using Json;
using Newtonsoft.Json.Linq;
using System.Data.Entity;
using MongoDB.Bson;

namespace WebAPIforCRUD.Controllers
{
    
    public class AttendanceController : ApiController
    {

        private MongoDBContext dbcontext;
        private IMongoCollection<attendanceViewModel> attendanceCollection;
        private IMongoCollection<studentViewModel> studentCollection;

        public AttendanceController()
        {
            dbcontext = new MongoDBContext();
            attendanceCollection = dbcontext.database.GetCollection<attendanceViewModel>("ATTENDANCE");
            studentCollection = dbcontext.database.GetCollection<studentViewModel>("STUDENT");


        }




        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public IHttpActionResult Get(string Class_id , string date)
        {
            // get all student accrding to class 
            var sfilter = Builders<studentViewModel>.Filter.Eq("Class_id", Class_id);
            IList<studentViewModel> stulist = studentCollection.Find(sfilter).ToList();


            var Filter = Builders<attendanceViewModel>.Filter.Eq("class_id", Class_id);
            IList<attendanceViewModel> list = attendanceCollection.Find(Filter).ToList();
            
            IList<attendanceViewModel1> flist = new List<attendanceViewModel1>();

            foreach(var student in stulist)
            {
                Boolean flag = false;
                foreach (var attendance in list)
                {
                    string tempDate = Convert.ToString(attendance.dateOfAttendance.AddDays(1)).Split(' ')[0];
                    if (student._id.ToString().Equals(attendance.studend_id) && string.Compare(date, tempDate) == 0) // student._id.ToString().Equals(attendance.studend_id) && 
                    {
                        flag = true;
                        break;                                                    
                    }                  
                }
                if (flag)
                {
                    foreach (var attendance in list)
                    { 
                        if (student._id.ToString().Equals(attendance.studend_id))
                        {
                            attendanceViewModel1 notAtten = new attendanceViewModel1();
                            notAtten.AttendanceMark = attendance.AttendanceMark;
                            notAtten.class_id = attendance.class_id;
                            notAtten.studend_id = attendance.studend_id;
                            notAtten.dateOfAttendance = Convert.ToDateTime(date);
                            //notAtten._id = attendance._id;
                            flist.Add(notAtten);
                            break;
                        }
                    }
                    
                }
                else
                {
                    attendanceViewModel1 notAtten = new attendanceViewModel1();
                    notAtten.AttendanceMark = "false";
                    notAtten.class_id = student.Class_id;
                    notAtten.studend_id = student._id.ToString();
                    notAtten.dateOfAttendance = Convert.ToDateTime(date);
                    //notAtten._id = ss._id;
                    flist.Add(notAtten);
                }
            }
        /*    attendanceViewModel a1 = null;
            foreach(var ss in list)
            {
                string temp = Convert.ToString(ss.dateOfAttendance).Split(' ')[0];
              //  var datess = ss.dateOfAttendance.Split('T')[0];
                if (string.Compare(date,temp)==0)
                {
                    a1 = new attendanceViewModel();
                    a1.AttendanceMark = ss.AttendanceMark;
                    a1.class_id = ss.class_id;
                    a1.studend_id = ss.studend_id;
                    a1.dateOfAttendance = ss.dateOfAttendance;
                    a1._id = ss._id;
                    flist.Add(a1);

                }else
                {
                    continue; 
                }
            }*/

            return Ok(flist); 
        }

        // POST api/<controller>
        public IHttpActionResult Post([FromBody]JArray value)
        {


            attendanceViewModel a1 = new attendanceViewModel();
            IList<attendanceViewModel> lsit = new List<attendanceViewModel>();
            foreach (var ss in value)
            {
                a1 = new attendanceViewModel();
                a1.studend_id = (string)ss["studend_id"];
                a1.AttendanceMark = (string)ss["AttendanceMark"];
                DateTime date = Convert.ToDateTime(ss["dateOfAttendance"]);
                a1.dateOfAttendance = (DateTime)date.AddDays(1);
                //a1.dateOfAttendance.AddDays(1);
                a1.class_id = (string)ss["class_id"];


                // convert to dateTimeformat
                var userdate = Convert.ToDateTime(a1.dateOfAttendance);


                var beginDate = date;       
                var endDate = beginDate.AddDays(1); 

                var filterList = attendanceCollection.AsQueryable<attendanceViewModel>().Where(x => (x.dateOfAttendance == userdate) && ((x.studend_id) == a1.studend_id)).ToList();

                if (filterList.Count == 0)
                {
                    attendanceCollection.InsertOneAsync(a1);
                }
                else
                {
                    attendanceViewModel a2 = filterList[0];
                    ObjectId id = a2._id;
                    var filter = Builders<attendanceViewModel>.Filter.Eq("_id", id);
                    var update = Builders<attendanceViewModel>.Update.Set("AttendanceMark", a1.AttendanceMark);

                    var result = attendanceCollection.UpdateOne(filter, update);
                }


                // lsit.Add(a1);
            }




            //   attendanceCollection.InsertManyAsync(lsit);


            return Ok("DOne");


        }
        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {

        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}