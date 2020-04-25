using StudentsEducation.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentsEducation.Domain.Interfaces
{
    public interface IRepository<T> where T:BaseEntity
    {
        public IEnumerable<T> GetAll();
        public T GetById(int id);
        public void Update(T entity);
        public void Delete(int id);
        public T Create(T entity);
        public T Find(int id);
    }
}
