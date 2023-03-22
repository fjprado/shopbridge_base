using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
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

        public async Task<IEnumerable<Product>> ListProducts(ProductRequestModel request)
        {
            try
            {
                request.Page = request.Page > 0 ? request.Page : 1;
                request.ItemsPerPage = request.ItemsPerPage > 0 ? request.ItemsPerPage : 10;

                _logger.LogInformation("Getting list of products");

                return await _productRepository.Get(x =>
                    (request.PriceStart == 0 || x.Product_Price >= request.PriceStart) &&
                    (request.PriceEnd == 0 || x.Product_Price <= request.PriceEnd),
                    skip: (request.Page - 1) * request.ItemsPerPage, take: request.ItemsPerPage);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }            
        }

        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            try
            {
                _logger.LogInformation("Getting all products");
                return await _productRepository.GetAll();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }            
        }

        public Product GetProduct(Guid id)
        {
            try
            {
                _logger.LogInformation("Getting product by id");
                if (_productRepository.Get(x => x.Product_Id == id).Result.IsNullOrEmpty())
                {
                    _logger.LogWarning("Product not found");
                    throw new Exception($"Product not found - Id: {id}");
                }                    

                return _productRepository.Get(x => x.Product_Id == id).Result.FirstOrDefault();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public Task<Product> Insert(Product entity)
        {
            try
            {                
                entity.Product_Id = Guid.NewGuid();
                _logger.LogInformation("Inserting product into database");
                return _productRepository.Insert(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }            
        }

        public bool Remove(Guid entity_id)
        {
            try
            {
                _logger.LogInformation("Getting product by id");
                var entity = _productRepository.Get(x => x.Product_Id == entity_id).Result.FirstOrDefault();
                if (entity == null)
                {
                    _logger.LogWarning("Product not found");
                    throw new Exception($"Product not found - Id: {entity_id}");
                }                    

                return _productRepository.Remove(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }            
        }

        public Product Update(Guid id, Product entity)
        {
            try
            {
                _logger.LogInformation("Getting product by id");
                if (_productRepository.Get(x => x.Product_Id == id).Result.IsNullOrEmpty())
                {
                    _logger.LogWarning("Product not found");
                    throw new Exception($"Product not found - Id: {id}");
                }                    

                return _productRepository.Update(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }                   
        }
    }
}
