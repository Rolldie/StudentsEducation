using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace StudentsEducation.Domain.Interfaces
{
    public interface IAsyncRepository<T>:IRepository<T> where T:class
    {
        public Task<IEnumerable<T>> GetAllAsync();
        public Task<IEnumerable<T>> GetDetachedAllAsync();
        public Task<T> GetByIdAsync(int id);
        public Task<T> CreateAsync(T entity);
        public Task DeleteAsync(int id);
        public Task UpdateAsync(T entity);
        public Task<T> FindAsync(int id);
        public Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, 
                IOrderedQueryable<T>> orderBy = null);


    }
}
