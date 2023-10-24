using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace PawMates.CORE.Interfaces
{
    public interface IRepository<T> where T : class, IEntity
    {
        Response<IEnumerable<T>> GetAll();
        Response<T> GetById(int id);
        Response<T> Add(T entity);
        Response Update(T entity);
        Response Delete(T entity);
        Response<IEnumerable<T>> GetAll(Func<T, bool> predicate);
        Response<T> GetOne(Func<T, bool> predicate);
    }
}
