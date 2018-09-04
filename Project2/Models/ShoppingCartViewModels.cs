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
		public decimal? Price { get; set; }
	}

	public class ShoppingCartVM
	{
		public int UserId { get; set; }
		public decimal Price { get; set; }
		public ICollection<ShoppingCartItemVM> Items { get; set; }

		public ShoppingCartVM(Mapping Map, int UserId)
		{
			Items = new List<ShoppingCartItemVM>();
			this.UserId = UserId;

			// Set the list of items - make sure that the items belong to the user
			var tempList = Map.ShoppingCartRepo.RetrieveAll()
				.Where(
					item => item.UserId == this.UserId
				);

			Price = 0.0m;
			foreach(var item in tempList)
			{
				// Add the item to the view list
				var itemVM = Map.Map(item);
				Items.Add(itemVM);
				// Pricing
				if(item.Standard)
				{
					// Standard priced item - just add it
					itemVM.Price = Map.StandardProductRepo.RetrieveById(item.ProductId).Ini(Map.StandardProductRepo);
				}
				else
				{
					// Pricing dependent
					itemVM.Price = Map.CustomPizzaRepo.RetrieveById(item.ProductId).Ini(Map.CustomPizzaRepo);
					
				}
				Price += (decimal)itemVM.Price * item.Quantity;
			}
		}
	}
}