using StudentsEducation.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StudentsEducation.Domain.Interfaces
{
    public interface IAsyncRepository<T>:IRepository<T> where T:BaseEntity
    {
        public Task<IEnumerable<T>> GetAllAsync();
        public Task<T> GetByIdAsync(int id);
        public Task<T> CreateAsync(T entity);
        public void DeleteAsync(int id);
        public void UpdateAsync(T entity);
        public Task<T> FindAsync(int id);


    }
}
