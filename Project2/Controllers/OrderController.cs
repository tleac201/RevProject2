using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using Project2;
using Project2.DAL;
using Project2.Models;

namespace Project2.Controllers
{
	[EnableCors(origins: "*", headers: "*", methods: "*")]
	[RoutePrefix("api/Order")]
	public class OrderController : ApiController
	{
		private Mapping map;
		private User _user;
		public OrderController()
		{
			map = new Mapping();
			_user = map.UserRepo.RetrieveById(1);
		}

		// GET: api/Order
		public IQueryable<Order> GetOrders()
		{
			return map.OrderRepo.RetrieveAll()
				.Where(order => order.UserId == _user.UserId
			).AsQueryable();
		}

		// GET: api/Order/5
		[ResponseType(typeof(Order))]
		public IHttpActionResult GetOrder(int id)
		{
			//Order order = db.Orders.Find(id);
			var order = map.OrderRepo.RetrieveById(id);

			if (order == null)
			{
				return NotFound();
			}

			if (order.UserId != _user.UserId)
			{
				return Unauthorized();
			}

			return Ok(order);
		}

		// PUT: api/Order/5
		[ResponseType(typeof(void))]
		public IHttpActionResult PutOrder(int id, OrderVM orderVM)
		{
			// ModelState
			if (!ModelState.IsValid && orderVM.Id != null)
			{
				return BadRequest(ModelState);
			}

			var order = map.OrderRepo.RetrieveById((int)orderVM.Id);

			if (order == null)
			{
				return NotFound();
			}

			// Authorize
			if (id != orderVM.Id)
			{
				return BadRequest();
			}

			map.OrderRepo.Update(order);

			try
			{
				map.OrderRepo.Save();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!OrderExists(id))
				{
					return NotFound();
				}
				else
				{
					throw;
				}
			}

			return StatusCode(HttpStatusCode.NoContent);
		}

		/*
		// POST: api/Order
		[ResponseType(typeof(Order))]
        public IHttpActionResult PostOrder(OrderVM orderVM)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

			if(orderVM.UserId != _user.UserId)
			{
				return Unauthorized();
			}

			// Initialize the data
			var order = map.Map(orderVM);
			order.OrderDate = DateTime.Now;
			map.OrderRepo.Insert(order);

			foreach(var od in orderVM.OrderDetails)
			{
				order.OrderDetails.Add(map.Map(od));
			}

            try
            {
				map.OrderRepo.Save();
            }
            catch (DbUpdateException)
            {
                if (OrderExists(order.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = order.Id }, order);
        }
		*/
		[ResponseType(typeof(OrderVM))] 
		[HttpPost]
		public IHttpActionResult PostOrder()
		{
			/*if(UserId != _user.UserId)
			{
				return Unauthorized();
			}*/

			if(map.UserRepo.RetrieveById(_user.UserId) == null)
			{
				return NotFound();
			}
			var scvm = new ShoppingCartVM(map, _user.UserId);

			// Make an order
			var order = new Order()
			{
				OrderDate = DateTime.Now,
				UserId = _user.UserId,
				User = _user,
			};
			map.OrderRepo.Insert(order);
			map.OrderRepo.Save();

			// Make Enter the order details.
			foreach(var item in scvm.Items)
			{
				order.OrderDetails.Add(
					new OrderDetail
					{
						ItemId = item.ProductId,
						Standard = item.Standard,
						Quantity = item.Quantity,
						OrderId = order.Id,
						Order = order
					}
				);
			}
			map.OrderDetailsRepo.Save();

			var scitems = map.ShoppingCartRepo.RetrieveAll()
				.Where(item => item.UserId == _user.UserId);
			foreach(var item in scitems)
			{
				map.ShoppingCartRepo.Delete(item);
			}
			map.ShoppingCartRepo.Save();
			return CreatedAtRoute("DefaultApi", new { id = order.Id }, order);
		}

        // DELETE: api/Order/5
        [ResponseType(typeof(Order))]
        public IHttpActionResult DeleteOrder(int id)
        {
            Order order = map.OrderRepo.RetrieveById(id);
            if (order == null)
            {
                return NotFound();
            }

			if(order.UserId != _user.UserId)
			{
				return Unauthorized();
			}

			// Delete dependencies for the order.
			foreach (var item in order.OrderDetails)
			{
				map.OrderDetailsRepo.Delete(item);
			}
			map.OrderDetailsRepo.Save();

			map.OrderRepo.Delete(order);
            map.OrderRepo.Save();

            return Ok(order);
        }

		/*
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                .Dispose();
            }
            base.Dispose(disposing);
        }
		*/
        private bool OrderExists(int id)
        {
            return map.OrderRepo.RetrieveAll().Count(e => e.Id == id) > 0;
        }
    }
}