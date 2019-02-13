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

        /*  [BsonId]
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
          */

            // Student Name 
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId _id { get; set; }
        [BsonElement("stuName")]
        [DataType(DataType.Text)]
        [Required]
        [MaxLength(20)]
        public string stuName { get; set; }

        // Email 
        [DataType(DataType.EmailAddress)]
        [BsonElement("email")]
        [Required]
        public string email { get; set; }

        // Passowrd
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Required")]
        [BsonElement("pwd")]
        [MinLength(6)]
        public string pwd { get; set; }

        // Mobile Number 
        [BsonElement("mobNo")]
        [MaxLength(10)]
        [DataType(DataType.PhoneNumber)]
        public string mobNo { get; set; }

        // Address
        [BsonElement("Address")]
        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "Required")]
        public string Address { get; set; }


        public string changeTime = DateTime.Now.ToString();

        public string updateTime = DateTime.Now.ToString();
        public string deleteTime= DateTime.Now.ToString();
        public string CreateTime = DateTime.Now.ToString();


    }
}