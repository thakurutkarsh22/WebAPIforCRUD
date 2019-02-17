using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPIforCRUD.Models;
using WebAPIforCRUD.App_Start;
using MongoDB.Driver;
using System.Web.Helpers;
using MongoDB.Bson;
using Microsoft.AspNetCore.Mvc;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace WebAPIforCRUD.Controllers
{
    [System.Web.Http.Authorize]
    [ApiController]
    public class StudentController : ApiController
    {
        private MongoDBContext dbcontext;
        private IMongoCollection<studentViewModel> studentCollection;

        public StudentController()
        {
            dbcontext = new MongoDBContext();
            studentCollection = dbcontext.database.GetCollection<studentViewModel>("STUDENT");

            
        }

        // Get Action Results 

        public IHttpActionResult getAllStudent()
        {
            
            var filter = Builders<studentViewModel>.Filter.Ne("changeTime", "null"); 

            IList<studentViewModel> students = studentCollection.Find(filter).ToList();



            IList<studentViewModel> lsit = studentCollection.AsQueryable<studentViewModel>().Where(a => a.changeTime != "null").ToList(); 

            if (students.Count == 0)
            {
                return NotFound();

            }

            return Ok(lsit);
            
            

        }

        public IHttpActionResult GetStudentById(string id)
        {
            studentViewModel student = null;
            var query_id = new ObjectId(id); 



            student = studentCollection.AsQueryable<studentViewModel>()
                    .Select(s => new studentViewModel()
                    {
                        _id = s._id,
                        stuName = s.stuName,
                        email = s.email

                    }).Where(s => s._id == query_id)
                 .SingleOrDefault(x => x._id == query_id);


            if (student == null)
            {
                return NotFound();
            }

            return Ok(student);
        }

        public IHttpActionResult GetStudentByNames(string namesss)
        {
          IList<studentViewModel> student = null;




            student = studentCollection.AsQueryable<studentViewModel>()
                    .Select(s => new studentViewModel()
                    {
                        _id = s._id,
                        stuName = s.stuName,
                        email = s.email

                    }).Where(s=>s.stuName == namesss)
                    .ToList<studentViewModel>();
               //  .SingleOrDefault(x => x.FirstName == namesss);


            if (student.Count == 0)
            {
                return NotFound();
            }

            return Ok(student);
        }

        public IHttpActionResult GetStudentByemail(string email)
        {
            studentViewModel student = null;




            student = studentCollection.AsQueryable<studentViewModel>()
                    .Select(s => new studentViewModel()
                    {
                        _id = s._id,
                        stuName = s.stuName,
                        email = s.email

                    }).Where(s => s.email == email)
                    
             .SingleOrDefault(x => x.email == email);


            if (student == null)
            {
                return NotFound();
            }

            return Ok(student);
        }

        // post action Result 
        [System.Web.Http.HttpPost]
        public IHttpActionResult PostNewStudent(studentViewModel student)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid data.");

            }
          
                studentCollection.InsertOne(new studentViewModel()
                {
                    _id = student._id,
                    stuName = student.stuName,
                    email = student.email,
                    pwd = student.pwd,
                    mobNo = student.mobNo,
                    Address = student.Address,
                    changeTime = student.changeTime,
                    updateTime = student.updateTime,
                    deleteTime = student.updateTime,
                    CreateTime = student.updateTime
                });
            return Ok("DOne");
        }

        // delete action result 

        public IHttpActionResult DeleteStudentby(string id)
        {
            var on = ObjectId.Parse(id); 
            var filter = Builders<studentViewModel>.Filter.Eq("_id",on );
            var update = Builders<studentViewModel>.Update.Set("changeTime", "null");
           
            studentCollection.UpdateOne(filter, update);

            return Ok("Voila Deleted");
        }

    
    }

}

