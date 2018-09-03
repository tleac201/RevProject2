using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project2.Models
{
	public class OrderDetailVM
	{
		public int? Id { get; set; }
		public int OrderId { get; set; }
		public bool Standard { get; set; }
		public int ItemId { get; set; }
		public int Quantity { get; set; }
	}
}