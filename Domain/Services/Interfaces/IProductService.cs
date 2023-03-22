using Shopbridge_base.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shopbridge_base.Domain.Services.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> ListProducts(ProductRequestModel request);
        Task<IEnumerable<Product>> GetAllProducts();
        Product GetProduct(Guid id);
        Task<Product> Insert(Product entity);
        Product Update(Guid id, Product entity);
        bool Remove(Guid entity_id);
    }
}
