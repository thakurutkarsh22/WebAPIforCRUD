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
            return new string[] { "value1tyy", "value2" };
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public IHttpActionResult Post([FromBody]JArray value)
        {


            attendanceViewModel a1 = new attendanceViewModel();
            IList<attendanceViewModel> lsit = new List<attendanceViewModel>();
            foreach (var ss in value)
            {
                a1 = new attendanceViewModel();
                a1.studend_id =(string) ss["studend_id"];
                a1.AttendanceMark = (Boolean)ss["AttendanceMark"];
                a1.dateOfAttendance = (DateTime)ss["dateOfAttendance"];

                lsit.Add(a1);
            }

    /*        attendanceViewModel a1 = new attendanceViewModel();
            a1.AttendanceMark = false;
            a1.dateOfAttendance = DateTime.Now;
            a1.studend_id = "5c655f1e2b38d6161c7cd10blusp";

            attendanceViewModel a2 = new attendanceViewModel();
            a2.AttendanceMark = true;
            a2.dateOfAttendance = DateTime.Now;
            a2.studend_id = "5c655f1e2b38d6161c7cd10bluspaa";

            IList<attendanceViewModel> lsit = new List<attendanceViewModel>();
            lsit.Add(a1);
            lsit.Add(a2);
     */
      //      JObject json = JObject.Parse(value); 
            

            attendanceCollection.InsertManyAsync(lsit);

           
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