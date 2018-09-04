using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project2.Models
{
	public class OrderVM
	{
		public int? Id { get; set; }
		public int UserId { get; set; }
		public System.DateTime? OrderDate { get; set; }
		public ICollection<OrderDetailVM> OrderDetails { get; set; }

		
	}
}