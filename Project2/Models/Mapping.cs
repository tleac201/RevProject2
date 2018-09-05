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
		public IOrderRepo OrderRepo;
		public IOrderDetailsRepo OrderDetailsRepo;

		public Mapping()
		{
			Project2Entities entities = new Project2Entities();
			ShoppingCartRepo = new ShoppingCartRepo(entities);
			UserRepo = new UserRepo(entities);
			CustomPizzaRepo = new CustomPizzaRepo(entities);
			StandardProductRepo = new StandardProductRepo(entities);
			CustIngredientsRepo = new CustIngredientsRepo(entities);
			OrderRepo = new OrderRepo(entities);
			OrderDetailsRepo = new OrderDetailRepo(entities);
		}

		#region Shopping Carts
		public ShoppingCartItem Map(ShoppingCartItemVM vm)
		{
			ShoppingCartItem item = new ShoppingCartItem
			{
				ProductId = vm.ProductId,
				Quantity = vm.Quantity,
				Standard = vm.Standard,
				DatePlaced = DateTime.Now,
			};

			if(vm.UserId != null)
			{
				item.UserId = (int)vm.UserId;
			}

			if (vm.Id != null)
			{
				item.Id = (int)vm.Id;
			}

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

		#region Orders
		public Order Map(OrderVM orderVM)
		{
			var order = new Order {
				UserId = orderVM.UserId,
			};
			//potentially that will be entered already
			if (orderVM.Id != null)
			{
				order.Id = (int)orderVM.Id;
			}

			//potentially entered already
			if (orderVM.OrderDate != null)
			{
				order.OrderDate = (DateTime)orderVM.OrderDate;
			}

			

			foreach (var od in orderVM.OrderDetails)
			{
				order.OrderDetails.Add(Map(od));
			}
			order.Ini(this);
			return order;
		}

		public OrderVM Map(Order order)
		{
			var orderVM = new OrderVM
			{
				Id = order.Id,
				UserId = order.UserId,
				OrderDate = order.OrderDate,				
			};

			//Convert the orderdetail items to be orderdetailvms
			foreach(var x in order.OrderDetails)
			{
				orderVM.OrderDetails.Add(Map(x));
			}

			return orderVM;
		}
		#endregion

		#region OrderDetails
		public OrderDetailVM Map(OrderDetail orderDetail)
		{
			var vm = new OrderDetailVM
			{
				Id = orderDetail.Id,
				ItemId = orderDetail.ItemId,
				OrderId = orderDetail.OrderId,
				Quantity = orderDetail.Quantity,
				Standard = orderDetail.Standard
			};
			return vm;
		}

		public OrderDetail Map(OrderDetailVM orderDetailVM)
		{
			var order = new OrderDetail
			{
				ItemId = orderDetailVM.ItemId,
				OrderId = orderDetailVM.OrderId,
				Quantity = orderDetailVM.Quantity,
				Standard = orderDetailVM.Standard,				
			};

			// Incase it's a new item
			if(null != orderDetailVM.Id)
			{
				order.Id = (int) orderDetailVM.Id;
			}

			return order;
		}
		#endregion

		#region Ingredients
		public IngredientVM Map(Ingredient ingredient)
		{
			return new IngredientVM
			{
				IngredientId = ingredient.IngredientId,
				IngredientName = ingredient.IngredientName,
				Description = ingredient.Description,
				Price = ingredient.Price
			};
		}
		#endregion
	}
}