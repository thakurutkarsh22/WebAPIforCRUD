using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson;

namespace WebAPIforCRUD.Models
{
    public class student
    {
       
        public ObjectId _id { get; set; }
        
        public string stuName { get; set; }

        // Email 
       
        public string email { get; set; }

        
        // Mobile Number 
        
        public string mobNo { get; set; }

        // Address
        
        public string Address { get; set; }


        public string changeTime = DateTime.Now.ToString();

        public string updateTime = DateTime.Now.ToString();
        public string deleteTime = DateTime.Now.ToString();
        public string CreateTime = DateTime.Now.ToString();

    }
}