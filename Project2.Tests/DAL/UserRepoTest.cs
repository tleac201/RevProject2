using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project2.DAL;

namespace Project2.Tests.DAL
{
    class UserRepoTest : IUserRepository
    {
        private IList<User> _users;
        public UserRepoTest()
        {
            _users = new List<User>();

            // Insert Seed data?
            /*
             _users.Add(new User
             {
                 UserId = ,
                 Active = ,
                 OpenDate = ,
                 CloseDate = ,
                 Email = ,
                 FirstName = ,
                 LastName = ,
                 Phone = 
             });
             */
        }
        public void Delete(User user)
        {
            user.CloseDate = DateTime.Now;
            user.Active = false;
            Update(user);
        }

        public void Insert(User user)
        {
            _users.Add(user);
        }

        public void Update(User user)
        {
            var updatedUser = RetrieveById(user.UserId);
            updatedUser = user;
        }

        public IEnumerable<User> RetrieveAll()
        {
            return _users;
        }

        public User RetrieveById(int id)
        {
            return _users.ToList().Find(user => user.UserId == id);
        }

        public void Save()
        {
            //Do nothing.
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
        // ~UserRepoTest() {
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

		public User RetrieveByEmail(string name)
		{
			throw new NotImplementedException();
		}
		#endregion
	}
}
