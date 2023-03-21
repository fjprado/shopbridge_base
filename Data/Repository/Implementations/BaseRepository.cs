using Microsoft.EntityFrameworkCore;
using Shopbridge_base.Data.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Shopbridge_base.Data.Repository.Implementations
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly Shopbridge_Context _context;
        public BaseRepository(Shopbridge_Context context)
        {
            _context = context;
        }

        public async Task<IEnumerable<T>> Get(Expression<Func<T, bool>>? expression = null, int? skip = null, int? take = null)
        {
            var query = _context.Set<T>().AsQueryable();

            if (expression != null)
                query = query.Where(expression);

            if (skip != null && skip.HasValue)
                query = query.Skip(skip.Value);

            if (take != null && take.HasValue)
                query = query.Take(take.Value);

            return await query.ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T> Insert(T entity)
        {
            var result = await _context.Set<T>().AddAsync(entity);
            _context.SaveChanges();
            return result.Entity;
        }

        public T Update(T entity)
        {
            var result = _context.Set<T>().Update(entity);
            _context.SaveChanges();
            return result.Entity;
        }

        public bool Remove(T entity)
        {
            var result = _context.Remove(entity).State;
            return result == EntityState.Deleted;
        }
    }
}
