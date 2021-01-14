using System.Collections.Generic;
using System.Threading.Tasks;
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
        [Route("{id}")]
        public async Task SaleBook(int id)
        {
            await _bookShop.SellBook(id);
        }        
        
        [HttpPut]
        [Route("startSale")]
        public void StartSale()
        {
            _bookShop.SaleByGenre();
        }        
        
        [HttpPut]
        [Route("stopSale")]
        public void StopSale()
        {
            _bookShop.CancelSaleByGenre();
        }
    }
}