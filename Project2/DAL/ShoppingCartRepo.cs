using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Project2.DAL
{
	public class ShoppingCartRepo : IShoppingCartRepository, IDisposable
	{
		public Project2Entities db;

		public ShoppingCartRepo(Project2Entities project2Entities)
		{
			db = project2Entities;
		}

		public IEnumerable<ShoppingCartItem> RetrieveAll()
		{
			return db.ShoppingCartItems;
		}

		public ShoppingCartItem RetrieveById(int id)
		{
			return db.ShoppingCartItems.Find(id);
		}

		public void Insert(ShoppingCartItem item)
		{
			db.ShoppingCartItems.Add(item);
		}


		public void Update(ShoppingCartItem item)
		{
			db.Entry(item).State = EntityState.Modified;
		}

		public void Delete(ShoppingCartItem item)
		{
			db.ShoppingCartItems.Remove(item);
		}

		public void Save()
		{
			db.SaveChanges();
		}

		#region IDisposable Support
		private bool disposedValue = false; // To detect redundant calls

		protected virtual void Dispose(bool disposing)
		{
			if (!disposedValue)
			{
				if (disposing)
				{
					db.Dispose();
				}

				this.disposedValue = true;
			}
		}

		// This code added to correctly implement the disposable pattern.
		public void Dispose()
		{
			// Do not change this code. Put cleanup code in Dispose(bool disposing) above.
			Dispose(true);
			// TODO: uncomment the following line if the finalizer is overridden above.
			GC.SuppressFinalize(this);
		}
		#endregion
	}
}