using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MongoDB.Bson;
using MongoDB.Driver;
using WebAPIforCRUD.App_Start;
using WebAPIforCRUD.Models;

namespace WebAPIforCRUD.Controllers
{
    public class SchoolController : ApiController
    {

        private MongoDBContext dbcontext;
        private IMongoCollection<schoolViewModel> schoolCollection;

        public SchoolController()
        {
            dbcontext = new MongoDBContext();
            schoolCollection = dbcontext.database.GetCollection<schoolViewModel>("SCHOOL");


        }


        // GET api/<controller>
        public IHttpActionResult GetallSchoolName()
        {
         //  var filter = Builders<schoolViewModel>.Filter.Ne("changeTime", "null");

          //  IList<schoolViewModel> students = schoolCollection.Find(filter).ToList();



            IList<schoolViewModel> lsit = schoolCollection.AsQueryable<schoolViewModel>().ToList();

            if ( lsit.Count == 0)
            {
                return NotFound();

            }

            return Ok(lsit);
        }

        // GET api/<controller>/5
        public IHttpActionResult GetById(string School_id)
        {
            schoolViewModel student = null;
            var query_id = new ObjectId(School_id);



            student = schoolCollection.AsQueryable<schoolViewModel>()
                   .Where(s => s._id == query_id)
                 .SingleOrDefault(x => x._id == query_id);


            if (student == null)
            {
                return NotFound();
            }

            return Ok(student);
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
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