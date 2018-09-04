using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project2.DAL
{
	public class CustomPizzaRepo : ICustomPizzaRepo
	{
		public Project2Entities db;

		public CustomPizzaRepo()
		{
		}

		public CustomPizzaRepo(Project2Entities db)
		{
			this.db = db;
		}

		public void Delete(CustomPizza pizza)
		{
			db.CustIngredients.RemoveRange(
				db.CustIngredients.Where(custI => custI.CPId == pizza.Id)
			);
			db.CustomPizzas.Remove(pizza);
		}

		public void Insert(CustomPizza pizza)
		{
			db.CustomPizzas.Add(pizza);
			db.CustIngredients.AddRange(pizza.CustIngredients);
		}

		public IEnumerable<CustomPizza> RetrieveAll()
		{
			return db.CustomPizzas;
		}

		public CustomPizza RetrieveById(int id)
		{
			return db.CustomPizzas.Find(id);
		}

		public void Save()
		{
			db.SaveChanges();
		}

		public void Update(CustomPizza pizza)
		{
			db.Entry(pizza).State = System.Data.Entity.EntityState.Modified;
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