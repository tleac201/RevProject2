// TO DO

/* Unstatically code user1 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using Project2.DAL;
using Project2.Models;

namespace Project2.Controllers
{
	[EnableCors(origins: "*", headers: "*", methods: "*")]
	// [Authorize]
	public class ShoppingCartController : ApiController
	{
		public User _user;
		public Mapping Map = new Mapping();

		public ShoppingCartController()
		{
			//var username = this.User.Identity.Name;
			//_user = Map.UserRepo.RetrieveByEmail(username);
			_user = Map.UserRepo.RetrieveById(1);
		}

		public ShoppingCartController(IUserRepository user
			, IShoppingCartRepository shoppingCartRepository)
		{
			Map.ShoppingCartRepo = shoppingCartRepository;
			Map.UserRepo = user;

		}

		// GET: api/ShoppingCart
		public ShoppingCartVM GetShoppingCartItems()
		{
			return new ShoppingCartVM(Map, _user.UserId);
		}

		// GET: api/ShoppingCart/5
		[ResponseType(typeof(ShoppingCartItem))]
		public IHttpActionResult GetShoppingCartItem(int id)
		{
			ShoppingCartItem shoppingCartItem = Map.ShoppingCartRepo.RetrieveById(id);

			if (shoppingCartItem == null || shoppingCartItem.UserId != _user.UserId)
			{
				return NotFound();
			}

			return Ok(shoppingCartItem);
		}

		// PUT: api/ShoppingCart/5
		[ResponseType(typeof(void))]
		public IHttpActionResult PutShoppingCartItem(int id, ShoppingCartItemVM shoppingCartItemVM)
		{
			if (!ModelState.IsValid || shoppingCartItemVM.Id == null)
			{
				return BadRequest(ModelState);
			}

			//Check that item belongs to user before editing
			if (id != shoppingCartItemVM.Id || shoppingCartItemVM.UserId != _user.UserId)
			{
				return BadRequest();
			}

			// All is good in the hood. Update table.

			var shoppingCartItem = Map.ShoppingCartRepo.RetrieveById((int)shoppingCartItemVM.Id);
			shoppingCartItem.Quantity = shoppingCartItemVM.Quantity;
			Map.ShoppingCartRepo.Update(shoppingCartItem);

			try
			{
				Map.ShoppingCartRepo.Save();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!ShoppingCartItemExists(id))
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
		
		// POST: api/ShoppingCart
		[ResponseType(typeof(ShoppingCartItem))]
		[TypeConverter(typeof(ShoppingCartItem))]
		public IHttpActionResult PostShoppingCartItem(ShoppingCartItemVM shoppingCartItemVM)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			if(shoppingCartItemVM.UserId != _user.UserId)
			{
				return Unauthorized();
			}

			shoppingCartItemVM.DatePlaced = DateTime.Now;

			// Map VM to an actual item.
			var mappedShoppingCartItem = Map.Map(shoppingCartItemVM);

			// Check if there is already a quantity of the product in the shopping cart
			// for the user.
			var shoppingCartItem = Map.ShoppingCartRepo.RetrieveAll().Where(
				QItem => QItem.UserId == _user.UserId
				&& QItem.ProductId == mappedShoppingCartItem.ProductId
				&& QItem.Standard == mappedShoppingCartItem.Standard
			).SingleOrDefault();

			// If there are already matching items - BadRequest
			if(shoppingCartItem != null)
			{
				return BadRequest("Edit quantities in shopping cart. Don't order more.");
			}

			//All is well. Make an item
			shoppingCartItem = Map.Map(shoppingCartItemVM);
			shoppingCartItem.DatePlaced = DateTime.Now;
			Map.ShoppingCartRepo.Insert(shoppingCartItem);
			Map.ShoppingCartRepo.Save();
				
			return CreatedAtRoute("DefaultApi", new { id = shoppingCartItem.Id }, shoppingCartItem);
		}

		// DELETE: api/ShoppingCart/5
		[ResponseType(typeof(ShoppingCartItem))]
		public IHttpActionResult DeleteShoppingCartItem(int id)
		{
			ShoppingCartItem shoppingCartItem = Map.ShoppingCartRepo.RetrieveById(id);
			if (shoppingCartItem == null || shoppingCartItem.User.UserId != _user.UserId)
            {
                return NotFound();
            }

            Map.ShoppingCartRepo.Delete(shoppingCartItem);
            Map.ShoppingCartRepo.Save();

            return Ok(shoppingCartItem);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Map.ShoppingCartRepo.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ShoppingCartItemExists(int id)
        {
            var find = Map.ShoppingCartRepo.RetrieveAll().ToList().Find(e => e.Id == id && e.UserId == _user.UserId);
			if(find != null)
			{
				return true;
			}
			return false;
        }
    }
}