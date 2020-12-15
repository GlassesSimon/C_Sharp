using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Task2;

namespace Task2Infrastructure.EntityFramework
{
    public class BooksContext : DbContext
    {
        public const string DefaultSchemaName = "public";

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
            var books = Set<Book>();
            var listBooks = books.ToListAsync();
            return await listBooks;
        }

        public void AddBooks(List<Book> books)
        {
            Set<Book>().AddRange(books);
        }
    }
}