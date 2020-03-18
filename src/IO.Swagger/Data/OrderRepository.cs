using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using IO.Swagger.Models;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace IO.Swagger.Data
{
    public class OrderRepository: IRepository<Order>
    {
        private IMongoDatabase _db;
        private IMongoCollection<Order> _dbCollection;
        
        
        //TODO: NEEDS TO BE PUSHED INTO A COMMON CLASS
        public OrderRepository(string connectionString)
        {
            var dbclient = new MongoClient(connectionString); // Setup MongoDB Connection string
            _db = dbclient.GetDatabase("CustomerDB");
            _dbCollection = _db.GetCollection<Order>("orders");  // Create Collection
        }
        
        public void Dispose()
        {
            _db = null;
        }

        public Order GetById(Guid id)
        {
            return  _dbCollection.Find(x => x.Id == id).First();
        }

        public Guid Create(Order entity)
        {
            entity.Id = Guid.NewGuid();
            entity.OrderId = entity.Id.ToString();
            _dbCollection.InsertOne(entity);
            return entity.Id;
        }

        public void Delete(Order entity)
        {
            _dbCollection.DeleteOne( x => x.Id == entity.Id);
        }

        public void Update(Order entity)
        {
            var ud = Builders<Order>.Update.Set("Description", entity.Description)
                .Set("ShipAddress", entity.Shipaddress)
                .Set("Customer", entity.Customer)
                .Set("Status", entity.Status)
                .Set("OrderItems", entity.OrderItems);
            _dbCollection.UpdateOneAsync(x => x.Id == entity.Id, ud);  
        }

        public IEnumerable<Order> Query(Expression<Func<Order, bool>> expression)
        {
            return _dbCollection.AsQueryable<Order>().Where(expression).ToList();
        }
    }
}