using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Shopbridge_base.Data.Repository.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {
        Task<IEnumerable<T>> Get(Expression<Func<T, bool>> expression = null,
                                   int? skip = null,
                                   int? take = null);
        Task<IEnumerable<T>> GetAll();
        Task<T> Insert(T entity);
        T Update(T entity);
        bool Remove(T entity);
    }
}

