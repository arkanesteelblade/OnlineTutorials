/*
*   Items Repository CURD Async methods for MongoDB
**/
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using Play.Catalog.Service.Entities;

namespace Play.Catalog.Service.Repositories
{
    public class ItemsRepository
    {
        //like a table name, but in mongodb it's a collection
        private const string collectionName = "items";
        private readonly IMongoCollection<Item> dbCollection;
        private readonly FilterDefinitionBuilder<Item> filterBuilder = Builders<Item>.Filter;

        public ItemsRepository()
        {
            var mongoClient = new MongoClient("mongodb://localhost:27017");
            var database = mongoClient.GetDatabase("Catalog");              //DB For Catalog Microservice
            dbCollection = database.GetCollection<Item>(collectionName);
        }

        //return all items in db
        //using asyn programming model.  All methods will be async
        public async Task<IReadOnlyCollection<Item>> GetAllAsync()
        {
            return await dbCollection.Find(filterBuilder.Empty).ToListAsync();
        }

        //return a specific item from the db
        public async Task<Item> GetAsync(Guid id)
        {
            FilterDefinition<Item> filter = filterBuilder.Eq(entity => entity.Id, id);
            return await dbCollection.Find(filter).FirstOrDefaultAsync();
        }

        //add item into db
        public async Task CreateAsync(Item entity)
        {
            if (entity == null)
                throw new ArgumentException(nameof(entity));

            await dbCollection.InsertOneAsync(entity);
        }

        //update an item
        public async Task UpdateAsync(Item entity)
        {
            if (entity == null)
                throw new ArgumentException(nameof(entity));

            FilterDefinition<Item> filter = filterBuilder.Eq(existingentity => existingentity.Id, entity.Id);
            await dbCollection.ReplaceOneAsync(filter, entity);
        }

        //remove item from db
        public async Task RemoveAsync(Guid id)
        {
            FilterDefinition<Item> filter = filterBuilder.Eq(entity => entity.Id, id);
            await dbCollection.DeleteOneAsync(filter);
        }
    }
}