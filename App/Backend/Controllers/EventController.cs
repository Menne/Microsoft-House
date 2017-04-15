using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.OData;
using Backend.Models;
using Microsoft.Azure.Mobile.Server;

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
		public Task<Event> PatchEvent(string id, Delta<Event> patch)
		{
			return UpdateAsync(id, patch);
		}

		// POST tables/TodoItem
		public async Task<IHttpActionResult> PostEvent(Event currentEvent)
		{
			Event current = await InsertAsync(currentEvent);
			return CreatedAtRoute("Tables", new { id = current.Id }, current);
		}

		// DELETE tables/TodoItem/48D68C86-6EA6-4C25-AA33-223FC9A27959
		public Task DeleteEvent(string id)
		{
			return DeleteAsync(id);
		}
	}
}
