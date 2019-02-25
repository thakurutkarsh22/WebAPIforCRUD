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
    [Authorize]
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
            List<studentViewModel> stulist = studentCollection.Find(sfilter).ToList();


            var Filter = Builders<attendanceViewModel>.Filter.Eq("class_id", Class_id);
            List<attendanceViewModel> list = attendanceCollection.Find(Filter).ToList();

            List<attendanceViewModel> attenList = attandanceList(list , date);

            List<attendanceViewModel1> a = mainFunction(stulist, attenList, date); 

            return Ok(a); 
        }

        private List<attendanceViewModel> attandanceList(IList<attendanceViewModel> list ,string date )
        {
            List<attendanceViewModel> tempList = new List<attendanceViewModel>();


            foreach (var ss in list)
            {
                string datefrommodel = Convert.ToString(ss.dateOfAttendance).Split(' ')[0];
                if (datefrommodel.CompareTo(date) == 0)
                {
                    tempList.Add(ss);
                }
                else
                {
                    continue;
                }
            }


            return tempList;
        }

        private List<attendanceViewModel1> mainFunction(List<studentViewModel> stulist,IList<attendanceViewModel> attenList ,string date)
        {
            List<attendanceViewModel1> flist = new List<attendanceViewModel1>();

            if (attenList.Count == 0)
            {
                flist = convertToJsonforattenLsitzero(stulist,date);
            }else
            {
                foreach(var ss in stulist)
                {
                    int count = 0;
                    foreach(var aa in attenList)
                    {
                        if (ss._id.ToString().CompareTo(aa.studend_id) == 0)
                        {
                            count++;
                            flist.Add(makeobject( ss._id.ToString(),ss.stuName,aa.AttendanceMark,ss.Class_id,date));
                        }
                    }
                    if (count == 0)
                    {
                        flist.Add(makeobject(ss._id.ToString(), ss.stuName ,"false",ss.Class_id,date));
                    }
                }
            }
            return flist;
        }

        private List<attendanceViewModel1> convertToJsonforattenLsitzero(List<studentViewModel> list, string date)
        {
            List<attendanceViewModel1> flist = new List<attendanceViewModel1>();
            attendanceViewModel1 a = null;
            foreach(var ss in list)
            {
                a = new attendanceViewModel1(ss._id.ToString() , ss.stuName , "false",  ss.Class_id,date);
                flist.Add(a);
            }

            return flist; 
        }

        private attendanceViewModel1 makeobject(string stu_id , string stu_name , string attendancemark , string class_id, string date)
        {
            attendanceViewModel1 a = new attendanceViewModel1(stu_id,stu_name,attendancemark,class_id ,date);
            return a; 
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
                //a1.dateOfAttendance.AddDays(1);
                a1.class_id = (string)ss["class_id"];


                // convert to dateTimeformat
                var userdate = Convert.ToDateTime(a1.dateOfAttendance);


                var beginDate = date;       
                var endDate = beginDate.AddDays(1); 

                var filterList = attendanceCollection.AsQueryable<attendanceViewModel>().Where(x => (x.dateOfAttendance == userdate) && ((x.studend_id) == a1.studend_id)).ToList();

                if (filterList.Count == 0)
                {
                    a1.dateOfAttendance = (DateTime)date;
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