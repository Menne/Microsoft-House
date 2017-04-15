using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.OData;
using Backend.DataObjects;
using Backend.Models;
using Microsoft.Azure.Mobile.Server;

namespace Backend
{
	public class ReservationController : TableController<Reservation>
	{
		protected override void Initialize(HttpControllerContext controllerContext)
		{
			base.Initialize(controllerContext);
			MobileServiceContext context = new MobileServiceContext();
			DomainManager = new EntityDomainManager<Reservation>(context, Request);
		}

		// GET tables/TodoItem
		public IQueryable<Reservation> GetAllReservations()
		{
			return Query();
		}

		// GET tables/TodoItem/48D68C86-6EA6-4C25-AA33-223FC9A27959
		public SingleResult<Reservation> GetReservation(string id)
		{
			return Lookup(id);
		}

		// PATCH tables/TodoItem/48D68C86-6EA6-4C25-AA33-223FC9A27959
		public Task<Reservation> PatchReservation(string id, Delta<Reservation> patch)
		{
			return UpdateAsync(id, patch);
		}

		// POST tables/TodoItem
		public async Task<IHttpActionResult> PostCarPark(Reservation reservation)
		{
			Reservation current = await InsertAsync(reservation);
			return CreatedAtRoute("Tables", new { id = current.Id }, current);
		}

		// DELETE tables/TodoItem/48D68C86-6EA6-4C25-AA33-223FC9A27959
		public Task DeleteCarPark(string id)
		{
			return DeleteAsync(id);      
		}	
	}
			
}
