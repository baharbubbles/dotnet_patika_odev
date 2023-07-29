using Exercise1.Models;
using Microsoft.EntityFrameworkCore;

namespace Exercise1.Repository;
public class ProductRepository : IProductRepository
{
    private readonly Exercise1DataContext _context;

    public ProductRepository(Exercise1DataContext context)
    {
        _context = context;
    }

    public async Task<List<Product>> GetProductsAsync()
    {
        return await _context.Products.ToListAsync();
    }

    public async Task<Product> GetProductAsync(int id)
    {
        return await _context.Products.FirstOrDefaultAsync(v => v.Id == id);
    }

    public async Task<Product> AddProductAsync(Product product)
    {
        _context.Products.Add(product);
        await _context.SaveChangesAsync();
        return product;
    }

    public async Task<Product> UpdateProductAsync(Product product)
    {
        _context.Entry(product).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return product;
    }

    public async Task<Product> DeleteProductAsync(int id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product == null)
        {
            return null;
        }

        _context.Products.Remove(product);
        await _context.SaveChangesAsync();
        return product;
    }

    public bool IsProductValid(Product product)
    {
        if (product == null || string.IsNullOrEmpty(product.Name) || product.Price <= 0)
            {
                return false;
            } else {
                return true;
            }
    }
}