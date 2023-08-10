using BookStore.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookStore.DBOperations
{
    public interface IBookStoreDbContext
    {
        DbSet<Book> Books { get; set; }
        DbSet<Genre> Genres { get; set; }
        DbSet<User> Users { get; set; }
        DbSet<Author> Authors { get; set; }

        int SaveChanges();
    }
}