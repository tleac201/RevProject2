using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project2.Models
{
	public class ShoppingCartItemVM
	{
		public int? Id { get; set; }
		public int UserId { get; set; }
		public int ProductId { get; set; }
		public DateTime? DatePlaced { get; set; }
		public bool Standard { get; set; }
		public int Quantity { get; set; }
	}
}