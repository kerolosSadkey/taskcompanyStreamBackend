using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public interface IRepository<T>
    {
        public T GetById(int id, params Expression<Func<T, object>>[] includes);

        IQueryable<T> GetAll(params Expression<Func<T, object>>[] includes);
        public IQueryable<T> GetAll();

        public void Add(T entity);

        public void Update(T entity);

        public void Delete(T entity);
    }
}
