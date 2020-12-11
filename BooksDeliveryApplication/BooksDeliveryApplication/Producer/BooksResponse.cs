using System.Collections.Generic;
using ContractBookShop;

namespace BooksDeliveryApplication.Producer
{
    public class BooksResponse: IBooksResponse
    {
        public List<IBooksResponse.Book> Books { get; set; }
    }
}