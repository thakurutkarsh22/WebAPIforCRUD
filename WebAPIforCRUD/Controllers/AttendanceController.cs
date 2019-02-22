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

        public AttendanceController()
        {
            dbcontext = new MongoDBContext();
            attendanceCollection = dbcontext.database.GetCollection<attendanceViewModel>("ATTENDANCE");


        }




        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public IHttpActionResult Get(string Class_id , string date)
        {
            var Filter = Builders<attendanceViewModel>.Filter.Eq("class_id", Class_id);
            IList<attendanceViewModel> list = attendanceCollection.Find(Filter).ToList();
            //  IList<attendanceViewModel> llist = attendanceCollection.AsQueryable<attendanceViewModel>()
            //    .Where(x => x.studend_id == id).ToList();
            IList<attendanceViewModel> flist = new List<attendanceViewModel>();
            attendanceViewModel a1 = null;
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
            }

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
                a1.dateOfAttendance = (DateTime)date;
                a1.class_id = (string)ss["class_id"];

                // convert to dateTimeformat
                var userdate = Convert.ToDateTime(a1.dateOfAttendance);


                var beginDate = date;       
                var endDate = beginDate.AddDays(1); 

                var filterList = attendanceCollection.AsQueryable<attendanceViewModel>().Where(x => (x.dateOfAttendance >= beginDate) && (x.dateOfAttendance <= endDate)
                && ((x.studend_id) == a1.studend_id)).ToList();

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