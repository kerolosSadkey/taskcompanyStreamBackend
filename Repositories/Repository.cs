using DataLayer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        public readonly SqlContext context;
        private readonly DbSet<T> _table;


        public Repository(SqlContext _context)
        {
            context = _context;
            _table = _context.Set<T>();
        }
        public void Add(T entity)
        {
            context.Add(entity);
           
        }

        public void Delete(T entity)
        {
            context.Remove(entity);
        }

        public IQueryable<T> GetAll(params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _table;

            if (includes != null)
                query = includes.Aggregate(query,
                    (current, include) => current.Include(include));

            return query;
        }
        public IQueryable<T> GetAll() => _table;

        public T GetById(int id, params Expression<Func<T, object>>[] includes)
        {
            return _table.Find(id);
        }

        public void Update(T entity)
        {
            context.Entry(entity).State = EntityState.Modified;
            context.Update(entity);
        }
    }
}
