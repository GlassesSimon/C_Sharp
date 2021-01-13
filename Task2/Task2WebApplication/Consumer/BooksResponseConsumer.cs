using System.Linq;
using System.Threading.Tasks;
using ContractBookShop;
using MassTransit;
using Task2;
using Task2WebApplication.Services;

namespace Task2WebApplication.Consumer
{
    public class BooksResponseConsumer: IConsumer<IBooksResponse>
    {
        private readonly BookShop _bookShop;

        public BooksResponseConsumer(BookShop bookShop)
        {
            _bookShop = bookShop;
        }
        public async Task Consume(ConsumeContext<IBooksResponse> context)
        {
            var message = context.Message;
            
            #warning тут всё-таки лучше не делать этот маппинг, а перенести его прям в метод, а аргументом туда передавать message 
            var books = message.Books.Select(book => new Book(0, book.Title, book.Genre, book.Price, book.IsNew, book.DateDelivery)).ToList();

            await _bookShop.ReceiveDelivery(books);
        }  
    }
}