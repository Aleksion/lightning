﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using coffeeinService.DataObjects;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents.Linq;
using Microsoft.WindowsAzure.Mobile.Service;

namespace coffeeinService.DocumentDbWiring
{
    public class BaseDocumentDomainManager<TDocument> where TDocument : BaseDocument
    {
        public HttpRequestMessage Request { get; set; }
        public ApiServices Services { get; set; }
 
 
        private string _collectionId;
        private string _databaseId;
        private Database _database;
        private DocumentCollection _collection;
        private DocumentClient _client;
 
        public BaseDocumentDomainManager(string collectionId, string databaseId, HttpRequestMessage request, ApiServices services)
        {
            Request = request;
            Services = services;
            _collectionId = collectionId;
            _databaseId = databaseId;
        }
 
        public async Task<bool> DeleteAsync(string id)
        {
            try
            {
                var doc = GetDocument(id);
 
 
                if (doc == null)
                {
                    return false;
                }
 
                await Client.DeleteDocumentAsync(doc.SelfLink);
 
                return true;
 
 
            }
            catch (Exception ex)
            {
                Services.Log.Error(ex);
                throw new HttpResponseException(System.Net.HttpStatusCode.InternalServerError);
            }
        }
 
        public async Task<Document> InsertAsync(TDocument data, Trigger trigger = null)
        {
            try
            {
                if(trigger == null) return await Client.CreateDocumentAsync(Collection.SelfLink, data);

                trigger = ReadOrCreateTrigger(trigger);

                return await Client.CreateDocumentAsync(Collection.SelfLink, data, new RequestOptions{PostTriggerInclude = new List<string>{trigger.Id}});
 
 
            }
            catch (Exception ex)
            {
                Services.Log.Error(ex);
                throw new HttpResponseException(System.Net.HttpStatusCode.InternalServerError);
            }
        }

 
        public TDocument Lookup(string id)
        {
            try
            {
                var doc = Client.CreateDocumentQuery<TDocument>(Collection.DocumentsLink)
                        .Where(d => d.Id == id)
                        .AsEnumerable()
                        .FirstOrDefault();

                if (doc != null) return doc;


                throw new HttpResponseException(System.Net.HttpStatusCode.NotFound);
 
 
            }
            catch (Exception ex)
            {
                Services.Log.Error(ex);
                throw ex;
            }
        }
 
        public IQueryable<TDocument> Query(string type)
        {
            try
            {
                return Client.CreateDocumentQuery<TDocument>(Collection.DocumentsLink).Where(d => d.Type == type);
            }
            catch (Exception ex)
            {
                Services.Log.Error(ex);
                throw new HttpResponseException(System.Net.HttpStatusCode.InternalServerError);
            }
        }

        public IQueryable<TDocument> Query()
        {
            try
            {
                return Client.CreateDocumentQuery<TDocument>(Collection.DocumentsLink);
            }
            catch (Exception ex)
            {
                Services.Log.Error(ex);
                throw new HttpResponseException(System.Net.HttpStatusCode.InternalServerError);
            }
        }

 
        public async Task<bool> ReplaceAsync(string id, TDocument item)
        {
 
            if (item == null || id != item.Id)
            {
                throw new HttpResponseException(System.Net.HttpStatusCode.BadRequest);
            }
 
            try
            {
                var doc = GetDocument(id);
 
                if (doc == null)
                {
                    return false;
                }
 
                await Client.ReplaceDocumentAsync(doc.SelfLink, item);
 
                return true;
 
            }
            catch (Exception ex)
            {
                Services.Log.Error(ex);
                throw new HttpResponseException(System.Net.HttpStatusCode.InternalServerError);
            }
        }
 
        private Document GetDocument(string id)
        {
            return Client.CreateDocumentQuery<Document>(Collection.DocumentsLink)
                        .Where(d => d.Id == id)
                        .AsEnumerable()
                        .FirstOrDefault();
        }
 
        #region DocumentDBClient
 
        private DocumentClient Client
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
 
        private DocumentCollection Collection
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
 
        private Database Database
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
 
        private DocumentCollection ReadOrCreateCollection(string databaseLink)
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

        private Trigger ReadOrCreateTrigger(Trigger _trigger)
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

        private Trigger ReplaceTrigger(Trigger item)
        {

            var prevTrigger = Client.CreateTriggerQuery(Collection.TriggersLink)
                              .Where(c => c.Id == item.Id)
                              .AsEnumerable()
                              .FirstOrDefault();

            prevTrigger.Body = item.Body;
            var trigger = Client.ReplaceTriggerAsync(prevTrigger).Result;

            return trigger;
        }

        private Database ReadOrCreateDatabase()
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
        #endregion
    
    }
}