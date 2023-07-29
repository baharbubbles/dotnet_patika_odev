using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Exercise1.Models;
using Exercise1.Repository;

namespace ProductController.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        

        // Ürünleri listeleme
        [HttpGet("list")]
        public async Task<ActionResult<List<Product>>> GetProducts()
        {
            var products = await _productRepository.GetProductsAsync();
            return Ok(products);
        }

        // Ürün getirme işlevi
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _productRepository.GetProductAsync(id);

            if (product != null)
            {
                return product;
            }
            else
            {
                return NotFound(new { message = "Product not found" });
            }
        }

        // Ürün ekleme işlevi
        [HttpPost]
        public async Task<ActionResult<Product>> AddProduct([FromBody] Product product)
        {
            if (_productRepository.IsProductValid(product) == false)
            {
                return BadRequest(new { message = "Missing required fields" });
            }

            product = await _productRepository.AddProductAsync(product);

            return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
        }

        // Ürün güncelleme işlevi
        [HttpPut("{id}")]
        public async Task<ActionResult<Product>> UpdateProduct(int id, [FromBody] Product product)
        {
            var existingProduct = await _productRepository.GetProductAsync(id);

            if (existingProduct == null)
            {
                return NotFound(new { message = "Product not found" });
            }

            if (_productRepository.IsProductValid(product) == false)
            {
                return BadRequest(new { message = "Missing required fields" });
            }

            existingProduct.Name = product.Name;
            existingProduct.Price = product.Price;

            existingProduct = await _productRepository.UpdateProductAsync(existingProduct);

            return Ok(existingProduct);
        }

        // Ürün silme işlevi
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            var product = await _productRepository.GetProductAsync(id);

            if (product == null)
            {
                return NotFound(new { message = "Product not found" });
            }

            await _productRepository.DeleteProductAsync(id);
            return NoContent();
        }
    }

}
