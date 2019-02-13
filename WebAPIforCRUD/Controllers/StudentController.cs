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

namespace WebAPIforCRUD.Controllers
{
    public class StudentController : ApiController
    {
        private MongoDBContext dbcontext;
        private IMongoCollection<studentViewModel> studentCollection;

        public StudentController()
        {
            dbcontext = new MongoDBContext();
            studentCollection = dbcontext.database.GetCollection<studentViewModel>("student");

            
        }

        // Get Action Results 

        public IHttpActionResult getAllStudent()
        {

            IList<studentViewModel> students = studentCollection.AsQueryable<studentViewModel>().Select(s => new studentViewModel()
            {
                Id = s.Id,
                FirstName = s.FirstName,
                SecondName = s.SecondName
                
            }).ToList();
            
         

            if (students.Count == 0)
            {
                return NotFound();

            }

            return Ok(students);
            
            

        }

        public IHttpActionResult GetStudentById(string id)
        {
            studentViewModel student = null;
            var query_id = new ObjectId(id); 



            student = studentCollection.AsQueryable<studentViewModel>()
                    .Select(s => new studentViewModel()
                    {
                        Id = s.Id,
                        FirstName = s.FirstName,
                        SecondName = s.SecondName
                    }).Where(s => s.Id == query_id)
                 .SingleOrDefault(x => x.Id == query_id);


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
                        Id = s.Id,
                        FirstName = s.FirstName,
                        SecondName = s.SecondName
                    }).Where(s=>s.FirstName == namesss)
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
                    {   Id=s.Id,
                        FirstName = s.FirstName,
                        SecondName = s.SecondName,
                        Email = s.Email
                    }).Where(s => s.Email == email)
                    
             .SingleOrDefault(x => x.Email == email);


            if (student == null)
            {
                return NotFound();
            }

            return Ok(student);
        }

        // post action Result 

        public IHttpActionResult PostNewStudent(studentViewModel student)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid data.");

            }
          
                studentCollection.InsertOne(new studentViewModel()
                {
                    Id = student.Id,
                    FirstName = student.FirstName,
                    SecondName = student.SecondName,
                    Email = student.Email,
                    Password = student.Password
                });
            return Ok("DOne");
        }

        // delete action result 

        public IHttpActionResult DeleteStudentby(string id)
        {
            var on = ObjectId.Parse(id); 
            var delete_obj = Builders<studentViewModel>.Filter.Eq("_id",on );

            studentCollection.DeleteOne(delete_obj);

            return Ok("Voila Deleted");
        }


    }

}

