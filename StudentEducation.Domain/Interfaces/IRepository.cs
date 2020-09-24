using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace StudentsEducation.Domain.Interfaces
{
    public interface IRepository<T> where T:class
    {
        public IEnumerable<T> GetAll();
        public T GetById(int id);
        public IEnumerable<T> Get(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, 
            IOrderedQueryable<T>> orderBy = null);
        public void Update(T entity);
        public void Delete(int id);
        public T Create(T entity);
        public T Find(int id);
    }
}
