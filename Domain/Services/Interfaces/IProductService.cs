using Shopbridge_base.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Shopbridge_base.Domain.Services.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> Get(Guid? id,
                                       decimal? priceStart,
                                       decimal? priceEnd,
                                       int? skip = null,
                                       int? take = null);
        Task<IEnumerable<Product>> GetAll();
        Task<Product> Insert(Product entity);
        Product Update(Product entity);
        bool Remove(Guid entity_id);
    }
}
