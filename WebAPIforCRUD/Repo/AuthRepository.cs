using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPIforCRUD.Models;
using WebAPIforCRUD.App_Start;
using MongoDB.Driver;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MongoDB.Bson;



namespace WebAPIforCRUD.Repo
{

    public interface IAuthRepository
    {
        Task<List<User>> GetUsers();

        Task<User> GetUserAsync(string login, string password);
    }


    public class AuthRepository : IAuthRepository
    {
        private MongoDBContext dbcontext;
        private IMongoCollection<User> usercollection;

        public AuthRepository()
        {
            dbcontext = new MongoDBContext();
            usercollection = dbcontext.database.GetCollection<User>("USER");
        }


        public async Task<User> GetUserAsync(string login, string password)
        {
            var builder = Builders<User>.Filter;
            var filter = builder.Eq("Login", login) & builder.Eq("Password", password);

            return await usercollection
                        .Find(filter)
                        .FirstOrDefaultAsync();
        }

        public Task<List<User>> GetUsers()
        {
            throw new NotImplementedException();
        }
    }
}
