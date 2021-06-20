using System.Collections.Generic;
using System.IO;

namespace Payment.Solution.DataAccessLayer.Interfaces
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> Get(string where = null);
        object Create(T entity);
        void Update(string where, string set);
        void Delete(object id);
    }
}
