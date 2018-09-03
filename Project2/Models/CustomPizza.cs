using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Project2.DAL;

namespace Project2
{
	public partial class CustomPizza
	{
		public decimal Ini(ICustomPizzaRepo customPizzaRepo)
		{
			decimal pizzaPrice = 10m;
			var freeCount = 0;
			foreach (var ingredient in CustIngredients)
			{
				if (freeCount > 1)
				{
					pizzaPrice += ingredient.Ingredient.Price;
				}
				freeCount++;
			}
			return pizzaPrice;
		}
	}
}