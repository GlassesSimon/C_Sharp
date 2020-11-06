using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Task2;

namespace Task2Infrastructure.EntityFramework
{
    public class BooksContext : DbContext
    {
        public const string DefaultSchemaName = "Books";

        public BooksContext(DbContextOptions options) : base(options)
        {
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
            modelBuilder.HasDefaultSchema(DefaultSchemaName);
        }

        public async Task<List<Book>> GetBooks()
        {
            return await Set<Book>()
                .ToListAsync();
        }

        public void AddBook(Book book)
        {
            Set<Book>().Add(book);
        }
    }
}