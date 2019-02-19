using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
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
    
     
    [Authorize]
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

        public string WhatinHeader()
        {
            var re = Request;
            var headers = re.Headers;
            if (headers.Contains("Authorization"))
            {
                string token = headers.GetValues("Authorization").First().Substring("Bearer ".Length).Trim();
                return decodeJwtToken(token); 
            }else
            {
                return "This is not a valid jwt token";
           }
        }

        public string decodeJwtToken(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(token) as JwtSecurityToken;
            var username = jsonToken.Claims.First().Value;
            return username;
        }

        // GET api/<controller>
        public async System.Threading.Tasks.Task<IHttpActionResult> Get()
        {
           var username =  WhatinHeader();

            var name = HttpContext.Current.Request.Form["Login"];
            var pass = HttpContext.Current.Request.Form["Password"];
            AuthRepository ap = new AuthRepository();
            User user = new User();
            user = await ap.GetUserAsync(username);
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