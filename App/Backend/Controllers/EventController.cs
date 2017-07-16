using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.OData;
using Backend.Models;
using Microsoft.Azure.Mobile.Server;
using Microsoft.Azure.Mobile.Server.Config;
using Microsoft.Azure.NotificationHubs;
using System.Collections.Generic;

namespace Backend
{
	public class EventController : TableController<Event>
	{
		protected override void Initialize(HttpControllerContext controllerContext)
		{
			base.Initialize(controllerContext);
			MobileServiceContext context = new MobileServiceContext();
			DomainManager = new EntityDomainManager<Event>(context, Request);
		}

		// GET tables/TodoItem/
		public IQueryable<Event> GetAllEvents()
		{
			return Query();
		}

		// GET tables/TodoItem/48D68C86-6EA6-4C25-AA33-223FC9A27959
		public SingleResult<Event> GetEvent(string id)
		{
			return Lookup(id);
		}

		// PATCH tables/TodoItem/48D68C86-6EA6-4C25-AA33-223FC9A27959
		public async Task<Event> PatchEvent(string id, Delta<Event> patch)
		{
			var item = await UpdateAsync(id, patch);
            await PushToSyncAsync("event", item.Id);
            return item;
        }

        // POST tables/TodoItem
        public async Task<IHttpActionResult> PostEvent(Event currentEvent)
		{
			Event current = await InsertAsync(currentEvent);
            await PushToSyncAsync("event", currentEvent.Id);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
		}

		// DELETE tables/TodoItem/48D68C86-6EA6-4C25-AA33-223FC9A27959
		public async Task DeleteEvent(string id)
		{
            await PushToSyncAsync("event", id);
            await DeleteAsync(id);
        }


        private async Task PushToSyncAsync(string table, string id)
        {
            var appSettings = this.Configuration.GetMobileAppSettingsProvider().GetMobileAppSettings();
            var nhName = appSettings.NotificationHubName;
            var nhConnection = appSettings.Connections[MobileAppSettingsKeys.NotificationHubConnectionString].ConnectionString;

            // Create a new Notification Hub client
            var hub = NotificationHubClient.CreateClientFromConnectionString(nhConnection, nhName);

            // Create a template message
            var templateParams = new Dictionary<string, string>();
            templateParams["op"] = "sync";
            templateParams["table"] = table;
            templateParams["id"] = id;

            // Send the template message
            try
            {
                var result = await hub.SendTemplateNotificationAsync(templateParams);
                Configuration.Services.GetTraceWriter().Info(result.State.ToString());
            }
            catch (Exception ex)
            {
                Configuration.Services.GetTraceWriter().Error(ex.Message, null, "PushToSync Error");
            }
        }

    }
}
