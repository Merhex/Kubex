using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Kubex.DAL.Repositories
{
    public interface IRepository<T, TKey> 
        where T : class
    {
        void Add(T entity);
        void AddRange(IEnumerable<T> entities);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);
        void Update(T entitiy);
        void UpdateRange(IEnumerable<T> entities);
        Task<T> Find(TKey key);
        IAsyncEnumerable<T> FindRange(Expression<Func<T, bool>> predicate);
        Task<bool> SaveAll();
    }
}