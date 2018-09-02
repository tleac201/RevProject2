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
using System.Web.Http.Description;
using Project2.DAL;
using Project2.Models;

namespace Project2.Controllers
{
	public class ShoppingCartController : ApiController
	{
		public User _user;
		public Mapping Map = new Mapping();

		public ShoppingCartController()
		{
			_user = Map.UserRepo.RetrieveById(1);
		}

		public ShoppingCartController(IUserRepository user
			, IShoppingCartRepository shoppingCartRepository)
		{
			Map.ShoppingCartRepo = shoppingCartRepository;
			Map.UserRepo = user;

		}

		// GET: api/ShoppingCart
		public IQueryable<ShoppingCartItem> GetShoppingCartItems()
		{
			return Map.ShoppingCartRepo.RetrieveAll().AsQueryable().Where(x => x.User.UserId == _user.UserId);
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
		public IHttpActionResult PutShoppingCartItem(int id, ShoppingCartItem shoppingCartItem)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			//Check that item belongs to user before editing
			if (id != shoppingCartItem.Id || shoppingCartItem.User.UserId != _user.UserId)
			{
				return BadRequest();
			}

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
			var shoppingCartItem = Map.Map(shoppingCartItemVM);
		
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