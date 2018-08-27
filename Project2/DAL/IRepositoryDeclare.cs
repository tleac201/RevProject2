using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project2.DAL
{
    public interface IUserRepository : IDisposable
    {
        void Insert(User user);
        void Delete(User user);
        void Update(User user);
        User RetrieveById(int id);
        IEnumerable<User> RetrieveAll();
        void Save();
    }
}