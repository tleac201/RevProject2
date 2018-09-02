using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Project2.DAL;
namespace Project2.Models
{
	public class Mapping
	{
		public IUserRepository UserRepo;
		public IShoppingCartRepository ShoppingCartRepo;
		public ICustomPizzaRepo CustomPizzaRepo;
		public IStandardProductRepo StandardProductRepo;

		public Mapping()
		{
			Project2Entities entities = new Project2Entities();
			ShoppingCartRepo = new ShoppingCartRepo(entities);
			UserRepo = new UserRepo(entities);
			CustomPizzaRepo = new CustomPizzaRepo(entities);
			StandardProductRepo = new StandardProductRepo(entities);
		}
		public ShoppingCartItem Map(ShoppingCartItemVM vm)
		{
			ShoppingCartItem item;

			//if shopping cart item exists - retrieve the object. Else
			// make new one and register with db.
			if (vm.Id != null)
			{
				item = ShoppingCartRepo.RetrieveById((int)vm.Id);
				item.SetItem();
			}
			else
			{
				item = new ShoppingCartItem
				{
					ProductId = vm.ProductId,
					Quantity = vm.Quantity,
					Standard = vm.Standard,
					User = UserRepo.RetrieveById(vm.UserId),
					DatePlaced = DateTime.Now,
					UserId = vm.UserId,
					standardProductRepo = StandardProductRepo
				};
				
				ShoppingCartRepo.Insert(item);
				item.SetItem();
				ShoppingCartRepo.Save();
			}
			
			return item;
		}

		public ShoppingCartItemVM Map(ShoppingCartItem item)
		{
			var vm = new ShoppingCartItemVM
			{
				Id = item.Id,
				UserId = item.UserId,
				ProductId = item.ProductId,
				DatePlaced = item.DatePlaced,
				Standard = item.Standard,
				Quantity = item.Quantity
			};
			return vm;
		}
	}
}