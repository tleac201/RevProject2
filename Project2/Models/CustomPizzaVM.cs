using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project2.Models
{
	public class CustomPizzaVM
	{
		public int? Id { get; set; }
		public int UserId { get; set; }
		public ICollection<CustIngredientVM> CustIngredients { get; set; }
	}
}