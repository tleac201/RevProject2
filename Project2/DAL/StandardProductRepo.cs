using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project2.DAL
{
	public class StandardProductRepo : IStandardProductRepo
	{
		public Project2Entities db;

		public StandardProductRepo()
		{
		}

		public StandardProductRepo(Project2Entities db)
		{
			this.db = db;
		}

		public void Delete(StandardProduct Product)
		{
			db.StandardProducts.Remove(Product);
		}

		public void Insert(StandardProduct Product)
		{
			db.StandardProducts.Add(Product);
		}

		public IEnumerable<StandardProduct> RetrieveAll()
		{
			return db.StandardProducts;
		}

		public StandardProduct RetrieveById(int id)
		{
			return db.StandardProducts.Find(id);
		}

		public void Save()
		{
			db.SaveChanges();
		}

		public void Update(StandardProduct Product)
		{
			db.Entry(Product).State = System.Data.Entity.EntityState.Modified;
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
		// ~StandardProductRepo() {
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