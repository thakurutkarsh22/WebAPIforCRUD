using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MongoDB.Driver;
using WebAPIforCRUD.App_Start;
using WebAPIforCRUD.Models;

namespace WebAPIforCRUD.Controllers
{
    
    public class ClassController : ApiController
    {


        private MongoDBContext dbcontext;
        private IMongoCollection<classViewModel> classCollection;

        public ClassController()
        {
            dbcontext = new MongoDBContext();
            classCollection = dbcontext.database.GetCollection<classViewModel>("CLASSES");


        }
        


        // GET api/<controller>
        [HttpGet]
        
        public IHttpActionResult Get(string User_id)
        {
            var Filter = Builders<classViewModel>.Filter.Eq("UserId", User_id);
            IList<classViewModel> list = classCollection.Find(Filter).ToList();
            //  IList<classViewModel> llist = classCollection.AsQueryable<classViewModel>()
            //    .Where(x => x.UserId == id).ToList();
            if (list.Count != 0)
            {
                return Ok(list);
            }
            else
            {
                return BadRequest("User with this Id was not found"); 
            }
            
        }

        // GET api/<controller>/5
    /*    public string Get(int id)
        {
            return "value";
        }
        */
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