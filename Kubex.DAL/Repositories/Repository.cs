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
            _context.Add<T>(entity);
        }

        public void AddRange(IEnumerable<T> entities) 
        {
            _context.AddRange(entities);
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
            _context.Remove<T>(entity);
        }

        public void RemoveRange(IEnumerable<T> entities) 
        {
            _context.RemoveRange(entities);
        }

        public void Update(T entity) 
        {
            _context.Update<T>(entity);
        }

        public void UpdateRange(IEnumerable<T> entities) 
        {
            _context.UpdateRange(entities);
        }

        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}