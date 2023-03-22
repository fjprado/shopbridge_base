using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Shopbridge_base.Domain.Models;
using Shopbridge_base.Domain.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shopbridge_base.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(IProductService productService, ILogger<ProductsController> logger)
        {
            _productService = productService;
            _logger = logger;
        }
       
        [HttpPost("list-products")]   
        public async Task<ActionResult<IEnumerable<Product>>> ListProducts(ProductRequestModel request)
        {
            try
            {
                _logger.LogInformation("Trying to get list of products");
                var result = await _productService.ListProducts(request);

                if (result.Any()) 
                    return Ok(result);

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }            
        }

        [HttpGet("get-all-products")]
        public async Task<ActionResult<IEnumerable<Product>>> GetAllProducts()
        {
            try
            {
                _logger.LogInformation("Trying to get all products");
                var result = await _productService.GetAllProducts();

                if (result.Any())
                    return Ok(result);

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("get-product/{id}")]
        public async Task<ActionResult<Product>> GetProduct(Guid id)
        {
            try
            {
                _logger.LogInformation("Trying to get a product");
                var result = _productService.GetProduct(id);

                if (result != null)
                    return Ok(result);

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
       
        [HttpPut("edit-product/{id}")]
        public async Task<IActionResult> PutProduct(Guid id, Product product)
        {
            try
            {
                _logger.LogInformation("Trying to update a product");
                var result = _productService.Update(id, product);

                if (result != null)
                    return Ok(result);

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        [HttpPost("add-product")]
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
            try
            {
                _logger.LogInformation("Trying to insert a product");
                var result = await _productService.Insert(product);

                if (result != null)
                    return Ok(result);

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        [HttpDelete("remove-product/{id}")]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            try
            {
                _logger.LogInformation("Trying to remove a product");
                return _productService.Remove(id) ? Ok("Product has been removed") : Ok("Product has not been removed");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
