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
using Project2.DAL;
using Project2.Models;

namespace Project2.Controllers
{
    public class CustomPizzasController : ApiController
    {
		private Mapping map = new Mapping();
		private User user;

		CustomPizzasController()
		{
			user = map.UserRepo.RetrieveById(1);
		}
        // GET: api/CustomPizzas
        public IQueryable<CustomPizza> GetCustomPizzas()
        {
			return map.CustomPizzaRepo.RetrieveAll().Where(
				pizza => pizza.UserId == user.UserId
			).AsQueryable();
        }

        // GET: api/CustomPizzas/5
        [ResponseType(typeof(CustomPizza))]
        public IHttpActionResult GetCustomPizza(int id)
        {
            CustomPizza customPizza = map.CustomPizzaRepo.RetrieveById(id);
            if (customPizza == null)
            {
                return NotFound();
            }

            return Ok(customPizza);
        }

        // PUT: api/CustomPizzas/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCustomPizza(int id, CustomPizzaVM customPizzaVM)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != customPizzaVM.Id)
            {
                return BadRequest();
            }

			if(customPizzaVM.UserId != user.UserId)
			{
				return Unauthorized();
			}

			var customPizza = map.CustomPizzaRepo.RetrieveById(id);
			var tempCP = map.Map(customPizzaVM);
			customPizza.CustIngredients = tempCP.CustIngredients;

			map.CustomPizzaRepo.Update(customPizza);

            try
            {
                map.CustomPizzaRepo.Save();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomPizzaExists(id))
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

        // POST: api/CustomPizzas
        [ResponseType(typeof(CustomPizza))]
        public IHttpActionResult PostCustomPizza(CustomPizzaVM customPizzaVM)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

			if(customPizzaVM.UserId != user.UserId)
			{
				return Unauthorized();
			}

			// All good. Map and insert.

			var customPizza = map.Map(customPizzaVM);
            map.CustomPizzaRepo.Insert(customPizza);
			map.CustomPizzaRepo.Save();

			//map.CustIngredientsRepo.Insert(customPizza.CustIngredients);
			//map.CustIngredientsRepo.Save();

            return CreatedAtRoute("DefaultApi", new { id = customPizza.Id }, customPizza);
        }

        // DELETE: api/CustomPizzas/5
        [ResponseType(typeof(CustomPizza))]
        public IHttpActionResult DeleteCustomPizza(int id)
        {
            CustomPizza customPizza = map.CustomPizzaRepo.RetrieveById(id);
            if (customPizza == null)
            {
                return NotFound();
            }

			if(customPizza.UserId != user.UserId)
			{
				return Unauthorized();
			}

			// Delete ingredients first - their dependencies won't let the pizza
			// be removed.
			var ingredientList = map.CustIngredientsRepo.RetrieveAll()
					.Where(item => item.CPId == id);
			foreach(var ingredient in ingredientList)
			{
				map.CustIngredientsRepo.Delete(ingredient);
			}
			map.CustIngredientsRepo.Save();

			// Then delete the pizza.
			map.CustomPizzaRepo.Delete(customPizza);
            map.CustomPizzaRepo.Save();

            return Ok(customPizza);
        }

		private bool CustomPizzaExists(int id)
		{
			return map.CustomPizzaRepo.RetrieveAll().Count(e => e.Id == id) > 0;
		}

		/*
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
		*/
	}
}