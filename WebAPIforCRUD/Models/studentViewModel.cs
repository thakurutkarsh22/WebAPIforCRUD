using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace WebAPIforCRUD.Models
{
    public class studentViewModel
    {

        [BsonId]
        public ObjectId Id { get; set; }

        [Required]
        [BsonElement("FirstName")]
        public string FirstName { get; set; }
        [Required]
        [BsonElement("SecondName")]
        public string SecondName { get; set; }
        [Required]
        [BsonElement("Email")]
        public string Email { get; set; }
        [Required]
        [BsonElement("Password")]
        public string Password { get; set; }

        
    }
}