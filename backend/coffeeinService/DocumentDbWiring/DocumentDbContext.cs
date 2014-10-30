using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents.Linq;

namespace coffeeinService.DocumentDbWiring
{
    public class DocumentDbContext
    {
        private string _collectionId;
        private string _databaseId;
        private Database _database;
        private DocumentCollection _collection;
        private DocumentClient _client;

        public DocumentDbContext(string collectionId, string databaseId)
        {
            _collectionId = collectionId;
            _databaseId = databaseId;
        }
        public DocumentClient Client
        {
            get
            {
                if (_client == null)
                {
                    string endpoint = ConfigurationManager.AppSettings["endpoint"];
                    string authKey = ConfigurationManager.AppSettings["authKey"];
                    Uri endpointUri = new Uri(endpoint);
                    _client = new DocumentClient(endpointUri, authKey);
                }

                return _client;
            }
        }


        public DocumentCollection Collection
        {
            get
            {
                if (_collection == null)
                {
                    _collection = ReadOrCreateCollection(Database.SelfLink);
                }

                return _collection;
            }
        }

        public Database Database
        {
            get
            {
                if (_database == null)
                {
                    _database = ReadOrCreateDatabase();
                }

                return _database;
            }
        }


        
        public DocumentCollection ReadOrCreateCollection(string databaseLink)
        {
            var col = Client.CreateDocumentCollectionQuery(databaseLink)
                              .Where(c => c.Id == _collectionId)
                              .AsEnumerable()
                              .FirstOrDefault();

            if (col == null)
            {
                col = Client.CreateDocumentCollectionAsync(databaseLink, new DocumentCollection { Id = _collectionId }).Result;
            }

            return col;
        }
        public async Task DeleteDatabase()
        {
            var col = await Client.DeleteDatabaseAsync(Database.SelfLink);
            _collection = null;
            _database = null;
            _client = null;

        }
        public Trigger ReadOrCreateTrigger(Trigger _trigger)
        {
            var trigger = Client.CreateTriggerQuery(Collection.TriggersLink)
                              .Where(c => c.Id == _trigger.Id)
                              .AsEnumerable()
                              .FirstOrDefault();

            if (trigger == null)
            {
                trigger = Client.CreateTriggerAsync(Collection.SelfLink, _trigger).Result;
            }

            return trigger;
        }

        public Trigger ReplaceTrigger(Trigger item)
        {

            var prevTrigger = Client.CreateTriggerQuery(Collection.TriggersLink)
                              .Where(c => c.Id == item.Id)
                              .AsEnumerable()
                              .FirstOrDefault();

            prevTrigger.Body = item.Body;
            var trigger = Client.ReplaceTriggerAsync(prevTrigger).Result;

            return trigger;
        }

        public
            Database ReadOrCreateDatabase()
        {
            var db = Client.CreateDatabaseQuery()
                            .Where(d => d.Id == _databaseId)
                            .AsEnumerable()
                            .FirstOrDefault();

            if (db == null)
            {
                db = Client.CreateDatabaseAsync(new Database { Id = _databaseId }).Result;
            }

            return db;
        }
    }
}