using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using NotesKeeper.Common;
using NotesKeeper.Common.Interfaces;
using NotesKeeper.Common.Models.Configuration;
using NotesKeeper.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesKeeper.DataAccess
{
    public class NoSqlContext<T> : IContext<T> where T : BaseModel
    {
        private readonly ApplicationConfiguration _applicationConfiguration;
        private ISerializer<T> _jsonSerializer;
        private IMongoDatabase _mongoDatabase;
        private const string MongoDbServerPrefix = "mongodb://";
        private readonly string CollectionName = $"{typeof(T).Name}Collection";

        public NoSqlContext(ISerializer<T> serializer, ApplicationConfiguration applicationConfiguration)
        {
            _jsonSerializer = serializer;
            _applicationConfiguration = applicationConfiguration;
        }

        public async Task<IEnumerable<T>> Create(IEnumerable<T> items)
        {
            var documents = items.Select(item => ParseItem(item));

            var collection = this._mongoDatabase.GetCollection<BsonDocument>(CollectionName);
            await collection.InsertManyAsync(documents).ConfigureAwait(false);
            return items;
        }

        public Task Delete(IEnumerable<Guid> ids = null)
        {
            return Task.Run(() =>
            {
                FilterDefinition<BsonDocument> filter;

                if (ids == null)
                {
                    filter = new BsonDocument();
                } else
                {
                    var objectIds = ids.Select(x => new ObjectId(x.ToString()));
                    var dict = new Dictionary<string, object>();
                    dict.Add("$in", objectIds);
                    filter = Builders<BsonDocument>.Filter.ElemMatch<BsonValue>("_id", new BsonDocument(dict));
                }

                var collection = this._mongoDatabase.GetCollection<BsonDocument>(CollectionName);
                collection.DeleteMany(filter);
            });
        }

        public Task OpenConnection()
        {
            return Task.Run(() =>
            {
                var client = new MongoClient($"{MongoDbServerPrefix}{this._applicationConfiguration.DbServer}");
                this._mongoDatabase = client.GetDatabase(this._applicationConfiguration.DbName);

                using (var cursor = this._mongoDatabase.ListCollectionNames())
                {
                    var listRepresentation = cursor.ToList();
                    if (!listRepresentation.Contains(CollectionName))
                    {
                        this._mongoDatabase.CreateCollection(CollectionName);
                    }
                }
            });
        }

        public Task<IEnumerable<T>> Read(IEnumerable<Guid> ids)
        {
            return Read(x => ids.Contains(x.Id));
        }

        public Task<IEnumerable<T>> Read(Func<T, bool> filter = null)
        {
            return Task.Run(() =>
            {
                var collection = this._mongoDatabase.GetCollection<BsonDocument>(CollectionName);
                var items = collection.AsQueryable().Select(x => ParseBson(x)).Where(x => filter(x));
                return items.AsEnumerable();
            });
        }

        public Task Update(IEnumerable<T> items)
        {
            return Task.Run(() =>
            {
                var collection = this._mongoDatabase.GetCollection<BsonDocument>(CollectionName);

                Parallel.ForEach(items, item =>
                {
                    var document = ParseItem(item);

                    var filter = new BsonDocument("_id", item.Id.ToString());
                    var result = collection
                        .UpdateOne(filter, new BsonDocument("$set", new BsonDocument(document.Elements.Where(elem => elem.Name != "_id"))));
                });
            });
        }

        private T ParseBson(BsonDocument doc)
        {
            var json = doc.ToJson();
            json = json.Replace("_id", "Id");
            return _jsonSerializer.Deserialize(json);
        }

        private BsonDocument ParseItem(T item)
        {
            var json = _jsonSerializer.Serialize(item).Replace("Id", "_id");
            var document = BsonSerializer.Deserialize<BsonDocument>(json);
            return document;
        }
    }
}
