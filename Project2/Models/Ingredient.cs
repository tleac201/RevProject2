using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project2.Models
{
	public class IngredientVM
	{
		public int IngredientId { get; set; }
		public string IngredientName { get; set; }
		public string Description { get; set; }
		public decimal Price { get; set; }
	}
}