using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Exercise1.Models;

namespace ProductController.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductController : ControllerBase
    {
        private List<Product> products = new List<Product>
        {
            new Product { Id = 1, Name = "Product 1", Price = 10.99 },
            new Product { Id = 2, Name = "Product 2", Price = 19.99 }
        };

        // Ürünleri listeleme ve sıralama işlevi
        [HttpGet("list")]
        public ActionResult<List<Product>> GetProducts([FromQuery] string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                var filteredProducts = products.FindAll(p => p.Name == name);
                filteredProducts.Sort((x, y) => x.Price.CompareTo(y.Price));
                return filteredProducts;
            }
            else
            {
                var sortedProducts = new List<Product>(products);
                sortedProducts.Sort((x, y) => x.Price.CompareTo(y.Price));
                return sortedProducts;
            }
        }

        // Ürün getirme işlevi
        [HttpGet("{id}")]
        public ActionResult<Product> GetProduct(int id)
        {
            var product = products.Find(p => p.Id == id);

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
        public ActionResult<Product> AddProduct([FromBody] Product product)
        {
            if (product == null || string.IsNullOrEmpty(product.Name) || product.Price <= 0)
            {
                return BadRequest(new { message = "Missing required fields" });
            }

            product.Id = products.Count + 1;
            products.Add(product);

            return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
        }

        // Ürün güncelleme işlevi
        [HttpPut("{id}")]
        public ActionResult<Product> UpdateProduct(int id, [FromBody] Product product)
        {
            var existingProduct = products.Find(p => p.Id == id);

            if (existingProduct == null)
            {
                return NotFound(new { message = "Product not found" });
            }

            if (product == null || string.IsNullOrEmpty(product.Name) || product.Price <= 0)
            {
                return BadRequest(new { message = "Missing required fields" });
            }

            existingProduct.Name = product.Name;
            existingProduct.Price = product.Price;

            return Ok(existingProduct);
        }

        // Ürün silme işlevi
        [HttpDelete("{id}")]
        public ActionResult DeleteProduct(int id)
        {
            var product = products.Find(p => p.Id == id);

            if (product == null)
            {
                return NotFound(new { message = "Product not found" });
            }

            products.Remove(product);
            return NoContent();
        }
    }

}
