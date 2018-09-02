using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Project2.DAL;
using Project2.Models;

namespace Project2
{
	public partial class ShoppingCartItem
	{
		CustomPizza CustomPizza { get; set; }
		StandardProduct StandardProduct { get; set; }
		
		public void SetItem()
		{
			Mapping map = new Mapping();
			if (Standard == true)
			{
				StandardProduct = map.StandardProductRepo.RetrieveById(ProductId);
			}
			else
			{
				CustomPizza = map.CustomPizzaRepo.RetrieveById(ProductId);
			}
		}
	}
}