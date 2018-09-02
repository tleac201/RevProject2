using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Project2.DAL;

namespace Project2
{
	public partial class ShoppingCartItem
	{
		CustomPizza CustomPizza { get; set; }
		StandardProduct StandardProduct { get; set; }
		IShoppingCartRepository shoppingCartRepository { get; set; }
		public IStandardProductRepo standardProductRepo { get; set; }
		public ICustomPizzaRepo customPizzaRepo { get; set; }
		
		public void SetItem()
		{
			
			if (Standard == true)
			{
				StandardProduct = standardProductRepo.RetrieveById(ProductId);
			}
			else
			{
				CustomPizza = customPizzaRepo.RetrieveById(ProductId);
			}
		}
	}
}