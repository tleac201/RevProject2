using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Data.Entity;
using Project2.Models;

namespace Project2.DAL
{
    public class UserRepo : IUserRepository
    {
		Project2Entities db;

		public UserRepo(Project2Entities project)
		{
			db = project;
		}
        
        public IEnumerable<User> RetrieveAll()
        {
            return db.Users;
        }

        public User RetrieveById(int id)
        {
            var user = db.Users.Find(id);
			return user;
        }

		public User RetrieveByEmail(string name)
		{
			var user = db.Users.SingleOrDefault(u => u.Email == name);
			return user;
		}

        public void Insert(User user)
        {
            db.Users.Add(user);
        }
        
        public void Update(User user)
        {
            db.Entry(user).State = EntityState.Modified;
        }
        
        public void Delete(User user)
        {
            user.CloseDate = DateTime.Now;
            user.Active = false;
            Update(user);
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