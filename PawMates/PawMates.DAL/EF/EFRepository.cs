using PawMates.CORE.Exceptions;
using PawMates.CORE.Interfaces;
using PawMates.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawMates.DAL.EF
{
    public class EFRepository<T> : IRepository<T> where T : class, IEntity
    {
        private readonly PawMatesContext _context;
        public EFRepository(PawMatesContext context)
        {
            _context = context;
        }
        public void Add(T entity)
        {
            try
            {
                _context.Add(entity);
                _context.SaveChanges();

            }
            catch (Exception ex)
            {

                throw new DALException("Unable to save entity", ex);
            }


        }

        public void Delete(T entity)
        {
            _context.Remove(entity);
            _context.SaveChanges();

        }

        public IEnumerable<T> GetAll()
        {
            return _context.Set<T>().ToList();
        }

        // Add a method to get all entities that match a predicate
        public IEnumerable<T> GetAll(Func<T, bool> predicate)
        {
            return _context.Set<T>().Where(predicate).ToList();
        }

        public T GetById(int id)
        {
            return _context.Set<T>().FirstOrDefault(x => x.Id == id);
        }

        // add a method to get one entity that matches a predicate
        public T? GetOne(Func<T, bool> predicate)
        {
            return _context.Set<T>().FirstOrDefault(predicate);
        }

        public void Update(T entity)
        {
            _context.Update(entity);
            _context.SaveChanges();
        }
    }
}
