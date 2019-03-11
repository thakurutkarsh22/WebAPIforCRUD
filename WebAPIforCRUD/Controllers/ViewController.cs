using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAPIforCRUD.App_Start;
using WebAPIforCRUD.Models;

namespace WebAPIforCRUD.Controllers
{
    public class ViewController : Controller
    {
        private MongoDBContext dbcontext;
        private IMongoCollection<Employee> usercollection;

        public ViewController()
        {
            dbcontext = new MongoDBContext();
            usercollection = dbcontext.database.GetCollection<Employee>("VIEW");
        }


        // GET: View
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddEmployee(Employee emp)
        {
            usercollection.InsertOneAsync(emp);

            return View();
        }
    }
}