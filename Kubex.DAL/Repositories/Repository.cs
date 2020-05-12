using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Kubex.DAL.Repositories
{
    public class Repository<T, TKey> : IRepository<T, TKey> 
        where T : class
    {
        protected readonly DataContext _context;

        public Repository(DataContext context)
        {
            _context = context;
        }

        public void Add(T entity)
        {
            _context.Set<T>().Add(entity);
        }

        public void AddRange(IEnumerable<T> entities) 
        {
            _context.Set<T>().AddRange(entities);
        }

        public async Task<T> Find(TKey key)
        {
            return await _context.Set<T>().FindAsync(key);
        }

        public async Task<IEnumerable<T>> FindRange(Expression<Func<T, bool>> predicate) 
        {
            return await _context.Set<T>().Where(predicate).ToListAsync();
        }

        public void Remove(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entities) 
        {
            _context.Set<T>().RemoveRange(entities);
        }

        public void Update(T entity) 
        {
            _context.Set<T>().Update(entity);
        }

        public void UpdateRange(IEnumerable<T> entities) 
        {
            _context.Set<T>().UpdateRange(entities);
        }

        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public void Attach(T entitity) 
        {
            _context.Set<T>().Attach(entitity);
        }
    }
}