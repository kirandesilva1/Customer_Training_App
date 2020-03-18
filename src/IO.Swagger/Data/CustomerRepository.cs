using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using IO.Swagger.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace IO.Swagger.Data
{
    public class CustomerRepository: IRepository<Customer> 
    {
        private IMongoDatabase _db;
        private IMongoCollection<Customer> _dbCollection;
        
        //TODO: NEEDS TO BE PUSHED INTO A COMMON CLASS
        public CustomerRepository(string connectionString)
        {
            var dbclient = new MongoClient(connectionString); // Setup MongoDB Connection string
            _db = dbclient.GetDatabase("CustomerDB");
            _dbCollection = _db.GetCollection<Customer>("customers");  // Create Collection
        }
        
        public void Dispose()
        {
            _db = null;
        }

        public Customer GetById(Guid id)
        {
            return  _dbCollection.Find(x => x.Id == id).First();
        }

        public Guid Create(Customer entity)
        {
            
                entity.Id = Guid.NewGuid();
                entity.CustomerId = entity.Id.ToString();
                _dbCollection.InsertOne(entity);
                return entity.Id;
        }

        public void Delete(Customer entity)
        {
            _dbCollection.DeleteOne( x => x.Id == entity.Id);
        }

        public void Update(Customer entity)
        {
            var ud = Builders<Customer>.Update.Set("Name", entity.Name)
                .Set("Address", entity.Address)
                .Set("NumberOfOrders", entity.Numberoforders);
            _dbCollection.UpdateOneAsync(x => x.Id == entity.Id, ud);  
        }

        public IEnumerable<Customer> Query(Expression<Func<Customer, bool>> expression)
        {
            return _dbCollection.AsQueryable<Customer>().Where(expression).ToList();
        }
    }
}