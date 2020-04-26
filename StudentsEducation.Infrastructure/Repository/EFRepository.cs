using Microsoft.EntityFrameworkCore;
using StudentsEducation.Domain.Entities;
using StudentsEducation.Domain.Interfaces;
using StudentsEducation.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace StudentsEducation.Infrastructure.Repository
{
    public class EFRepository<T> : IAsyncRepository<T>, IRepository<T> where T :class
    {
        private readonly EducationDbContext _context;
        private readonly DbSet<T> entityList;
        public EFRepository(EducationDbContext context)
        {
            _context = context;
            entityList = _context.Set<T>();
        }

        public T Create(T entity)
        {
            var result = entityList.Add(entity).Entity;
            _context.SaveChanges();
            return result;
        }

        public async Task<T> CreateAsync(T entity)
        {
            var result = await entityList.AddAsync(entity);
            await _context.SaveChangesAsync();
            return result.Entity;
        }

        public void Delete(int id)
        {
            entityList.Remove(entityList.Find(id));
            _context.SaveChanges();
        }

        public  async void DeleteAsync(int id)
        {
            entityList.Remove(await entityList.FindAsync(id));
            await _context.SaveChangesAsync();
        }

        public T Find(int id)=> entityList.Find(id);

        public async Task<T> FindAsync(int id)=> await entityList.FindAsync(id);

        public IEnumerable<T> Get(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null)
        {
            IQueryable<T> query = _context.Set<T>();
            if (filter != null) query=query.Where(filter);
            if (orderBy != null) return orderBy(query);
            else return query.ToList<T>();
        }
        public async Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null)
        {
            IQueryable<T> query = _context.Set<T>();
            if (filter != null) query = query.Where(filter);
            if (orderBy != null) return orderBy(query);
            else return await query.ToListAsync<T>();
        }

        public IEnumerable<T> GetAll()=> entityList.AsEnumerable();

        public async Task<IEnumerable<T>> GetAllAsync()=> await entityList.ToListAsync(); 

        public T GetById(int id)=> entityList.Find(id);

        public async Task<T> GetByIdAsync(int id)=> await entityList.FindAsync(id);

        public void Update(T entity)
        {
            entityList.Update(entity);
            _context.SaveChanges();
        }

        public async void UpdateAsync(T entity)
        {
            entityList.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
