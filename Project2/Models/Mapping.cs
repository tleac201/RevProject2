using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
		public ICustIngredientsRepo CustIngredientsRepo;

		public Mapping()
		{
			Project2Entities entities = new Project2Entities();
			ShoppingCartRepo = new ShoppingCartRepo(entities);
			UserRepo = new UserRepo(entities);
			CustomPizzaRepo = new CustomPizzaRepo(entities);
			StandardProductRepo = new StandardProductRepo(entities);
			CustIngredientsRepo = new CustIngredientsRepo(entities);
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
			var pizza = new CustomPizza
			{
				UserId = pizzaVM.UserId,
				User = UserRepo.RetrieveById(pizzaVM.UserId)
			};

			if(pizzaVM.Id != null)
			{
				pizza.Id = (int)pizzaVM.Id;
			}

			foreach (var ing in pizzaVM.CustIngredients)
			{
				pizza.CustIngredients.Add(new CustIngredient
				{
					IngredientId = ing.IngredientId,
					CPId = pizza.Id,
				});
			}
			return pizza;
		}

		public CustomPizzaVM Map(CustomPizza pizza)
		{
			var VM = new CustomPizzaVM
			{
				Id = pizza.Id,
				UserId = pizza.UserId
			};

			var ingredientsVM = new Collection<CustIngredientVM>();
			foreach(var ingredient in pizza.CustIngredients)
			{
				ingredientsVM.Add(new CustIngredientVM {
					IngredientId = ingredient.IngredientId,
					CPId = ingredient.CPId,
					Id = ingredient.Id
				});
			}
			return VM;
		}
		#endregion
	}
}