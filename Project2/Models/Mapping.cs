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

		#region Shopping Carts
		public ShoppingCartItem Map(ShoppingCartItemVM vm)
		{
			ShoppingCartItem item = new ShoppingCartItem
			{
				ProductId = vm.ProductId,
				Quantity = vm.Quantity,
				Standard = vm.Standard,
				User = UserRepo.RetrieveById(vm.UserId),
				DatePlaced = DateTime.Now,
				UserId = vm.UserId,
			};
			// Set the object refernece to a corresponding CP or SP
			// item.SetItem();
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
		#endregion

		#region CustomPizza
		public CustomPizza Map(CustomPizzaVM pizzaVM)
		{
			return new CustomPizza
			{
				Id = pizzaVM.Id,
				UserId = pizzaVM.UserId,
				CustIngredients = pizzaVM.CustIngredients,
				User = UserRepo.RetrieveById(pizzaVM.UserId)
			};
		}

		public CustomPizzaVM Map(CustomPizza pizza)
		{
			return new CustomPizzaVM
			{
				Id = pizza.Id,
				CustIngredients = pizza.CustIngredients,
				UserId = pizza.UserId
			};
		}
		#endregion
	}
}