using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project2.DAL
{
	public class OrderDetailRepo : IOrderDetailsRepo, IDisposable
	{
		public Project2Entities db;

		public OrderDetailRepo(Project2Entities db)
		{
			this.db = db;
		}

		public void Delete(OrderDetail OrderDetail)
		{
			db.OrderDetails.Remove(OrderDetail);
		}

		public void Insert(OrderDetail OrderDetail)
		{
			db.OrderDetails.Add(OrderDetail);
		}

		public IEnumerable<OrderDetail> RetrieveAll()
		{
			return db.OrderDetails;
		}

		public OrderDetail RetrieveById(int id)
		{
			return db.OrderDetails.Find(id);
		}

		public void Save()
		{
			db.SaveChanges();
		}

		public void Update(OrderDetail OrderDetail)
		{
			db.Entry(OrderDetail).State = System.Data.Entity.EntityState.Modified;
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
		// ~OrderDetailRepo() {
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