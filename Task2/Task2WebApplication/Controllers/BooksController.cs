using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Task2;
using Task2Infrastructure.EntityFramework;

namespace Task2WebApplication.Controllers
{
    [Route("api/books")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly BooksContextDbContextFactory _dbContextFactory;

        public BooksController(BooksContextDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }
        [HttpGet]
        public async Task<List<Book>> GetBooks()
        {
            await using var context = _dbContextFactory.GetContext();
            return await context.GetBooks();
        }
        
#warning если следовать REST идеологии, то тут не нужно указывать путь /add. 
#warning post /api/book как бы предполагает, что это добавление книги
        [Route("add")]
        [HttpPost]
        public async Task AddBook([FromBody] Book book)
        {
            await using var context = _dbContextFactory.GetContext();
            context.AddBook(book);
            await context.SaveChangesAsync();
        }
    }
}