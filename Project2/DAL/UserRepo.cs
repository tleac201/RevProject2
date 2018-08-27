using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Project2.Models;
namespace Project2.DAL
{
    public class UserRepo : IUserRepository
    {
        Project2Entities db = new Project2Entities();

        public IEnumerable<User> RetrieveAll()
        {
            return db.Users;
        }

        public User RetrieveById(int id)
        {
            return db.Users.Find(id);
        }

        public void Insert(User user)
        {
            db.Users.Add(user);
        }

        public void Update(User user)
        {
            var dbUser = db.Users.Find(user.UserId);
            dbUser = user;
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

                disposedValue = true;
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