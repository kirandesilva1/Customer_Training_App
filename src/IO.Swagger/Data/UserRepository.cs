using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using IO.Swagger.Models;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace IO.Swagger.Data
{
    public class UserRepository : IRepository<User> 
    {
        private IMongoDatabase _db;
        private IMongoCollection<User> _dbCollection;
        
        //TODO: NEEDS TO BE PUSHED INTO A COMMON CLASS
        public UserRepository(string connectionString)
        {
            var dbclient = new MongoClient(connectionString); // Setup MongoDB Connection string
            _db = dbclient.GetDatabase("CustomerDB");
            _dbCollection = _db.GetCollection<User>("users");  // Create Collection
        }
        
        public void Dispose()
        {
            _db = null;
        }

        public User GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Guid Create(User entity)
        {
            entity.Id = Guid.NewGuid();
            entity.UserId = entity.Id.ToString();
            _dbCollection.InsertOne(entity);
            return entity.Id;
            
        }

        public void Delete(User entity)
        {
            throw new NotImplementedException();
        }

        public void Update(User entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> Query(Expression<Func<User, bool>> expression)
        {
            return _dbCollection.AsQueryable<User>().Where(expression).ToList();
        }
    }
}