using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Task2;
using Task2WebApplication.Services;

namespace Task2WebApplication.Controllers
{
    [Route("api/books")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly BookShop _bookShop;

        public BooksController(BookShop bookShop)
        {
            _bookShop = bookShop;
        }
        [HttpGet]
        public List<Book> GetBooks()
        {
            return _bookShop.GetBooks();
        }
        
        [HttpPut]
        public void SaleBook([FromBody] Book book)
        {
            _bookShop.SellBook(book.Id);
        }
    }
}