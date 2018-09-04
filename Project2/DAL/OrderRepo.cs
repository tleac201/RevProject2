using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project2.DAL
{
	public class OrderRepo : IOrderRepo, IDisposable
	{
		public Project2Entities db;

		public OrderRepo(Project2Entities db)
		{
			this.db = db;
		}

		public void Insert(Order Order)
		{
			db.Orders.Add(Order);
		}

		public void Delete(Order Order)
		{
			db.Orders.Remove(Order);
		}

		public void Update(Order Order)
		{
			db.Entry(Order).State = System.Data.Entity.EntityState.Modified;
		}

		public IEnumerable<Order> RetrieveAll()
		{
			return db.Orders;
		}

		public Order RetrieveById(int id)
		{
			return db.Orders.Find(id);
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
					// TODO: dispose managed state (managed objects).
				}

				// TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
				// TODO: set large fields to null.

				disposedValue = true;
			}
		}

		// TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
		// ~OrderRepo() {
		//   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
		//   Dispose(false);
		// }

		// This code added to correctly implement the disposable pattern.
		public void Dispose()
		{
			// Do not change this code. Put cleanup code in Dispose(bool disposing) above.
			Dispose(true);
			// TODO: uncomment the following line if the finalizer is overridden above.
			// GC.SuppressFinalize(this);
		}
		#endregion
	}
}