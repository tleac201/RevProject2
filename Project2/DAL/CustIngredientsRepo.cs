using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project2.DAL
{
	public class CustIngredientsRepo : ICustIngredientsRepo, IDisposable
	{
		private Project2Entities db;
		public CustIngredientsRepo(Project2Entities entities)
		{
			db = entities;
		}

		public void Delete(CustIngredient Ingredient)
		{
			db.CustIngredients.Remove(Ingredient);
		}

		public void Insert(CustIngredient Ingredient)
		{
			db.CustIngredients.Add(Ingredient);
		}

		public void Insert(ICollection<CustIngredient> Ingredients)
		{
			db.CustIngredients.AddRange(Ingredients);
		}

		public IEnumerable<CustIngredient> RetrieveAll()
		{
			return db.CustIngredients;
		}

		public CustIngredient RetrieveById(int id)
		{
			return db.CustIngredients.Find(id);
		}

		public void Update(CustIngredient Ingredient)
		{
			db.Entry(Ingredient).State = System.Data.Entity.EntityState.Modified;
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
		// ~CustIngredientsRepo() {
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