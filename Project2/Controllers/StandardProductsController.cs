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

namespace Project2.Controllers
{
    public class StandardProductsController : ApiController
    {
        private Project2Entities db = new Project2Entities();

        // GET: api/StandardProducts
        public IQueryable<StandardProduct> GetStandardProducts()
        {
            return db.StandardProducts;
        }

        // GET: api/StandardProducts/5
        [ResponseType(typeof(StandardProduct))]
        public IHttpActionResult GetStandardProduct(int id)
        {
            StandardProduct standardProduct = db.StandardProducts.Find(id);
            if (standardProduct == null)
            {
                return NotFound();
            }

            return Ok(standardProduct);
        }

        // POST: api/StandardProducts
        [ResponseType(typeof(StandardProduct))]
        public IHttpActionResult PostStandardProduct(StandardProduct standardProduct)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.StandardProducts.Add(standardProduct);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = standardProduct.Id }, standardProduct);
        }
    }
}