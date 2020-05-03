﻿using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System.Threading.Tasks;
using smartLiving.Models;
using smartLiving.MongoDb;

namespace smartLiving.Repostries
{
    public class PropertyRepositry:InterfaceDataBase
    {
        private MongoDbContext dbContext = null;
        public PropertyRepositry(IConfiguration config)
        {

            dbContext = MongoDbContext.getMongoDbContext(config);//geting singletone object of database
            collection = dbContext.getDataBase().GetCollection<Property>("Property");//use singletone object to get database 
            //and call that database to get collection of Property
        }
        private readonly IMongoCollection<Property> collection;

        public async Task<object> retriveAllData()
        {
            return await collection.Find(x => true).ToListAsync();
        }

        //not defined yet because we will not delete in server we only disable particular account
        public async Task<object> delete(string id)
        {

            return true;
        }

        public async Task<object> insert(object obj)
        {
            Property Property = (Property)obj;
            
            await collection.InsertOneAsync((Property)Property);
            return true;

        }

        public async Task<object> retrieve(string pId)
        {
            var Property = Builders<Property>.Filter.Eq("PropertyId", pId);
            return await collection.Find(Property).ToListAsync();
        }

        public async Task<object> retrieveAll(string societyId)
        {
            var Property = Builders<Property>.Filter.Eq("societyId", societyId);
            return await collection.Find(Property).ToListAsync();
        }

        public async Task<object> update(string id, object Property)
        {
            await collection.ReplaceOneAsync(ZZ => ZZ.propertyId == id, (Property)Property);
            return true;
        }
    }


}