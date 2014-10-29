using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using AutoMapper.QueryableExtensions;
using coffeeinService.DataObjects;
using Microsoft.Azure.Documents;
using Microsoft.WindowsAzure.Mobile.Service;

namespace coffeeinService.DocumentDbWiring
{
    public abstract class UserDocumentController<TDocument> : ApiController where TDocument : Resource
    {
        public ApiServices Services { get; set; }

        private UserDocumentDomainManager domainManager;
        /// <summary>
        /// Gets or sets the <see cref="T:Microsoft.WindowsAzure.Mobile.Service.Tables.IDomainManager`1" /> to be used for accessing the backend store.
        /// </summary>
        protected UserDocumentDomainManager DomainManager
        {
            get
            {
                if (this.domainManager == null)
                {
                    throw new InvalidOperationException("Domain manager not set");
                }
                return this.domainManager;
            }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("value");
                }
                this.domainManager = value;
            }
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="T:Microsoft.WindowsAzure.Mobile.Service.TableController`1" /> class.
        /// </summary>
        protected UserDocumentController()
        {
        }


        protected virtual IQueryable<User> Query()
        {
            IQueryable<User> result;
            try
            {
                result = this.DomainManager.Query();
            }
            catch (HttpResponseException exception)
            {
                Services.Log.Error(exception, base.Request, LogCategories.Controllers);
                throw;
            }
            catch (Exception ex)
            {
                Services.Log.Error(ex, base.Request, LogCategories.Controllers);
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
            return result;
        }

        protected virtual User Lookup(string id)
        {

            try
            {
                return this.DomainManager.Lookup(id);
            }
            catch (HttpResponseException exception)
            {
                Services.Log.Error(exception, base.Request, LogCategories.TableControllers);
                throw;
            }
            catch (Exception ex)
            {
                Services.Log.Error(ex, base.Request, LogCategories.TableControllers);
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }

        }

        protected async virtual Task<User> InsertAsync(User item)
        {
            if (item == null)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }


            try
            {
                return await this.DomainManager.InsertAsync(item);

            }
            catch (HttpResponseException exception)
            {
                Services.Log.Error(exception, base.Request, LogCategories.TableControllers);
                throw;
            }
            catch (Exception ex)
            {
                Services.Log.Error(ex, base.Request, LogCategories.TableControllers);

                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }

        }

   
        protected virtual async Task DeleteAsync(string id)
        {
            bool flag = false;
            try
            {
                flag = await this.DomainManager.DeleteAsync(id);

            }
            catch (HttpResponseException exception)
            {
                Services.Log.Error(exception, base.Request, LogCategories.TableControllers);
                throw;
            }
            catch (Exception ex)
            {
                Services.Log.Error(ex, base.Request, LogCategories.TableControllers);

                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
            if (!flag)
            {
                Services.Log.Warn("Resource not found", base.Request);
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }


        }

    }
}