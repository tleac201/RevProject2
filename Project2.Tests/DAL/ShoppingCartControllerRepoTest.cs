using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project2.DAL;
namespace Project2.Tests.DAL
{
	class ShoppingCartControllerRepoTest : IShoppingCartRepository
	{
		public void Delete(StandardProduct standard)
		{
			throw new NotImplementedException();
		}

		public void Delete(CustomPizza pizza)
		{
			throw new NotImplementedException();
		}

		public void Insert(StandardProduct standard)
		{
			throw new NotImplementedException();
		}

		public void Insert(CustomPizza pizza)
		{
			throw new NotImplementedException();
		}

		public ShoppingCartItem RetrieveById(int id)
		{
			throw new NotImplementedException();
		}

		public void Update(StandardProduct standard)
		{
			throw new NotImplementedException();
		}

		public void Update(CustomPizza pizza)
		{
			throw new NotImplementedException();
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
		// ~ShoppingCartTest() {
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

		public void Insert(ShoppingCartItem item)
		{
			throw new NotImplementedException();
		}

		public void Delete(ShoppingCartItem item)
		{
			throw new NotImplementedException();
		}

		public void Update(ShoppingCartItem item)
		{
			throw new NotImplementedException();
		}

	public IEnumerable<ShoppingCartItem> RetrieveAll()
	{
		throw new NotImplementedException();
	}

	public void Save()
	{
		throw new NotImplementedException();
	}
	#endregion
}
}
