using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using IO.Swagger.Models;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace IO.Swagger.Data
{
    public class TokenRepository: IRepository<Token>
    {
        private IMongoDatabase _db;
        private IMongoCollection<Token> _dbCollection;
        
        //TODO: NEEDS TO BE PUSHED INTO A COMMON CLASS
        public TokenRepository(string connectionString)
        {
            var dbclient = new MongoClient(connectionString); // Setup MongoDB Connection string
            _db = dbclient.GetDatabase("CustomerDB");
            _dbCollection = _db.GetCollection<Token>("tokens");  // Create Collection
        }
        
        public void Dispose()
        {
            _db = null;
        }

        public Token GetById(Guid id)
        {
            return  _dbCollection.Find(x => x.Id == id).First();
        }

        public Guid Create(Token entity)
        {
            entity.Id = Guid.NewGuid();
            _dbCollection.InsertOne(entity);
            return entity.Id;
        }

        public void Delete(Token entity)
        {
            _dbCollection.DeleteOne( x => x.Id == entity.Id);
        }

        public void Update(Token entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Token> Query(Expression<Func<Token, bool>> expression)
        {
            return _dbCollection.AsQueryable<Token>().Where(expression).ToList();
        }
    }
}