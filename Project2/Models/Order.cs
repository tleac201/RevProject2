using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Project2.Models;

namespace Project2
{
	public partial class Order
	{
		public void Ini(Mapping map)
		{
			this.Price = 0;
			foreach (var item in this.OrderDetails)
			{
				decimal itemPrice;
				if (item.Standard)
				{
					itemPrice = item.Price;
				}
				else
				{
					var cp = map.CustomPizzaRepo.RetrieveById(item.ItemId);
					itemPrice = cp.Ini(map.CustomPizzaRepo);
				}
				this.Price += itemPrice * item.Quantity;
			}
		}
	}
}