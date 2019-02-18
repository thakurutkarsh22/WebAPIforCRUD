using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using MongoDB.Driver;
using WebAPIforCRUD.App_Start;
using WebAPIforCRUD.Models;
using WebAPIforCRUD.Repo;

namespace WebAPIforCRUD.Controllers
{
    public class MeController : ApiController
    {

        private MongoDBContext dbcontext;
        private IMongoCollection<meViewModel> studentCollection;

        public MeController()
        {
            dbcontext = new MongoDBContext();
            studentCollection = dbcontext.database.GetCollection<meViewModel>("USER");


        }

        public class JSONMAKER
        {
            public string Name { get; set; }
            public string ID { get; set; }
        }

        // GET api/<controller>
        public async System.Threading.Tasks.Task<IHttpActionResult> Get()
        {
            var name = HttpContext.Current.Request.Form["Login"];
            var pass = HttpContext.Current.Request.Form["Password"];
            AuthRepository ap = new AuthRepository();
            User user = new User();
            user = await ap.GetUserAsync(name, pass);
            if (user != null)
            {
                JSONMAKER jmaker = new JSONMAKER();
                jmaker.ID = user.Id.ToString();
                jmaker.Name = user.Name;
                return Ok(jmaker);
            }
            else
            {
                return BadRequest("No user Found");
            }
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
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