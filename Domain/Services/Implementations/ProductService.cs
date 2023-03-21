using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Microsoft.Extensions.Logging;
using Shopbridge_base.Data.Repository.Interfaces;
using Shopbridge_base.Domain.Models;
using Shopbridge_base.Domain.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Shopbridge_base.Domain.Services
{
    public class ProductService : IProductService
    {
        private readonly ILogger<ProductService> _logger;
        private readonly IProductRepository _productRepository;
        public ProductService(
            IProductRepository productRepository,
            ILogger<ProductService> logger)
        {
            _productRepository = productRepository;
            _logger = logger;
        }

        public async Task<IEnumerable<Product>> Get(Guid? id, decimal? priceStart = 0, decimal? priceEnd = 0, int? skip = null, int? take = null)
        {       
            if(id == null)
                throw new Exception($"Product Id not found");

            return await _productRepository.Get(
                x => x.Product_Id == id &&
                (priceStart == 0 || x.Product_Price >= priceStart) &&
                (priceEnd == 0 || x.Product_Price <= priceEnd),
                skip: skip, take: take);
        }

        public Task<IEnumerable<Product>> GetAll()
        {
            return _productRepository.GetAll();
        }

        public Task<Product> Insert(Product entity)
        {
            entity.Product_Id = Guid.NewGuid();
            entity.Product_StockBalance ??= 0;

            return _productRepository.Insert(entity);
        }

        public bool Remove(Guid entity_id)
        {
            var entity = _productRepository.Get(x => x.Product_Id == entity_id).Result.FirstOrDefault();
            if (entity == null)
                throw new Exception($"Product not found - Id: {entity.Product_Id}");

            return _productRepository.Remove(entity);
        }

        public Product Update(Product entity)
        {
            var entityFound = _productRepository.Get(x => x.Product_Id == entity.Product_Id).Result.FirstOrDefault();
            if (entityFound == null)
                throw new Exception($"Product not found - Id: {entity.Product_Id}");

            entity.Product_StockBalance ??= entityFound.Product_StockBalance;

            return _productRepository.Update(entity);           
        }
    }
}
