using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Project2;
using Project2.Models;

/*
 * remove item
	proceed to order
	Retrieve
    */
namespace Project2.Controllers
{
    public class ShoppingCartItemsController : ApiController
    {
        private Project2Entities db = new Project2Entities();

        // GET: api/ShoppingCartItems
        public IQueryable<ShoppingCartItem> GetShoppingCartItems()
        {
            return db.ShoppingCartItems;
        }

        // GET: api/ShoppingCartItems/5
        [ResponseType(typeof(ShoppingCartItem))]
        public IHttpActionResult GetShoppingCartItem(int id)
        {
            User user = new User();
            ShoppingCartItem shoppingCartItem = db.ShoppingCartItems.Find(id);
            user.UserId = shoppingCartItem.UserId;

            if (shoppingCartItem == null)
            {
                return NotFound();
            }

            return Ok(shoppingCartItem);
        }

        // PUT: api/ShoppingCartItems/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutShoppingCartItem(int id, ShoppingCartItem shoppingCartItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != shoppingCartItem.Id)
            {
                return BadRequest();
            }

            db.Entry(shoppingCartItem).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
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

        [Route("api/ShoppingCartItems/Quantity")]
        public IHttpActionResult PutShoppingCartQuantity(IEnumerable<ShoppingCartItem> cartItems)
        {
            User user = new User();

            foreach (var i in cartItems)
            {
                if (i.UserId == user.UserId)
                {
                    if (i.Quantity != 0)
                    {
                        var dbSCI = db.ShoppingCartItems.Find(i.Id);
                        dbSCI.Quantity = i.Quantity;
                    }
                    else
                    {
                        db.ShoppingCartItems.Remove(i);
                    }
                }
            }
            db.SaveChanges();
            return Ok();
        }

        // POST: api/ShoppingCartItems
        [ResponseType(typeof(ShoppingCartItem))]
        public IHttpActionResult PostShoppingCartItem(ShoppingCartItem shoppingCartItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ShoppingCartItems.Add(shoppingCartItem);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = shoppingCartItem.Id }, shoppingCartItem);
        }

        // DELETE: api/ShoppingCartItems/5
        [ResponseType(typeof(ShoppingCartItem))]
        public IHttpActionResult DeleteShoppingCartItem(int id)
        {
            ShoppingCartItem shoppingCartItem = db.ShoppingCartItems.Find(id);
            if (shoppingCartItem == null)
            {
                return NotFound();
            }

            db.ShoppingCartItems.Remove(shoppingCartItem);
            db.SaveChanges();

            return Ok(shoppingCartItem);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ShoppingCartItemExists(int id)
        {
            return db.ShoppingCartItems.Count(e => e.Id == id) > 0;
        }
    }
}