namespace Exercise1.Models;

using Microsoft.EntityFrameworkCore;

public class Exercise1DataContext : DbContext
{
    public Exercise1DataContext(DbContextOptions<Exercise1DataContext> options) : base(options)
    {
    }
    public DbSet<Product> Products { get; set; }
}