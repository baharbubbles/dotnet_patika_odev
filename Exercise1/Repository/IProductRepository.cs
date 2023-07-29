using Exercise1.Models;

namespace Exercise1.Repository;
public interface IProductRepository
{
    Task<List<Product>> GetProductsAsync();
    Task<Product> GetProductAsync(int id);
    Task<Product> AddProductAsync(Product product);
    Task<Product> UpdateProductAsync(Product product);
    Task<Product> DeleteProductAsync(int id);
    bool IsProductValid(Product product);
}