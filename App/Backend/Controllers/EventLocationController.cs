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
	public class EventLocationController :  TableController<EventLocation>
	{
		protected override void Initialize(HttpControllerContext controllerContext)
		{
			base.Initialize(controllerContext);
			MobileServiceContext context = new MobileServiceContext();
			DomainManager = new EntityDomainManager<EventLocation>(context, Request);
		}

		// GET tables/TodoItem
		public IQueryable<EventLocation> GetAllCarParks()
		{
			return Query();
		}

		// GET tables/TodoItem/48D68C86-6EA6-4C25-AA33-223FC9A27959
		public SingleResult<EventLocation> GetCarPark(string id)
		{
			return Lookup(id);
		}

		// PATCH tables/TodoItem/48D68C86-6EA6-4C25-AA33-223FC9A27959
		public Task<EventLocation> PatchCarPark(string id, Delta<EventLocation> patch)
		{
			return UpdateAsync(id, patch);
		}

		// POST tables/TodoItem
		public async Task<IHttpActionResult> PostEventLocation(EventLocation location)
		{
			EventLocation current = await InsertAsync(location);
			return CreatedAtRoute("Tables", new { id = current.Id }, current);
		}

		// DELETE tables/TodoItem/48D68C86-6EA6-4C25-AA33-223FC9A27959
		public Task DeleteEventLocation(string id)
		{
			return DeleteAsync(id);		
		}	
	}
}
