using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project2.DAL
{
    public interface IUserRepository : IDisposable
    {
        void Insert(User user);
        void Delete(User user);
        void Update(User user);
        User RetrieveById(int id);
        IEnumerable<User> RetrieveAll();
        void Save();
    }

	public interface IShoppingCartRepository : IDisposable
	{
		void Insert(ShoppingCartItem item);
		void Delete(ShoppingCartItem item);
		void Update(ShoppingCartItem item);
		IEnumerable<ShoppingCartItem> RetrieveAll();
		ShoppingCartItem RetrieveById(int id);
		void Save();
	}

	public interface ICustomPizzaRepo : IDisposable
	{
		void Insert(CustomPizza pizza);
		void Delete(CustomPizza pizza);
		void Update(CustomPizza pizza);
		IEnumerable<CustomPizza> RetrieveAll();
		CustomPizza RetrieveById(int id);
		void Save();
	}

	public interface IStandardProductRepo : IDisposable
	{
		void Insert(StandardProduct Product);
		void Delete(StandardProduct Product);
		void Update(StandardProduct Product);
		IEnumerable<StandardProduct> RetrieveAll();
		StandardProduct RetrieveById(int id);
		void Save();
	}
}